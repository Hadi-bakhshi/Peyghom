using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Peyghom.Modules.Chat.Features.CreateGroupChat;
using Peyghom.Modules.Chat.Features.SendMessage;
using System.Security.Claims;
using Peyghom.Modules.Chat.Features.AddParticipant;
using Peyghom.Modules.Chat.Features.AddReaction;
using Peyghom.Modules.Chat.Features.DeleteMessage;
using Peyghom.Modules.Chat.Features.EditMessage;
using Peyghom.Modules.Chat.Features.GetUserChats;
using Peyghom.Modules.Chat.Features.IsUserParticipant;
using Peyghom.Modules.Chat.Features.MarkMessagesAsRead;
using Peyghom.Modules.Chat.Features.RemoveParticipant;
using Peyghom.Modules.Chat.Features.RemoveReaction;

namespace Peyghom.Modules.Chat.Hubs;

[Authorize]
public class ChatHub(ISender sender, ILogger<ChatHub> _logger) : Hub
{
    private string UserId => Context.UserIdentifier
                             ?? Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                             ?? throw new UnauthorizedAccessException("User not found");

    // ========================================
    // CONNECTION LIFECYCLE
    // ========================================

    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation("User {UserId} connected", UserId);

        // Join user to their personal group
        await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{UserId}");

        // Join user to their chats
        var userChats = await sender.Send(new GetUserChatsQuery(UserId));
        foreach (var chat in userChats.Value)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"chat_{chat.Id}");
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("User {UserId} disconnected", UserId);
        await base.OnDisconnectedAsync(exception);
    }

    // ========================================
    // MESSAGE OPERATIONS
    // ========================================

    public async Task SendMessage(SendMessageRequest request)
    {
        var command = new SendMessageCommand(
            UserId,
            request.ChatId,
            request.Content,
            request.MessageType,
            request.ReplyToMessageId,
            request.Attachments);

        var result = await sender.Send(command);

        await Clients.Group($"chat_{request.ChatId}")
            .SendAsync("MessageReceived", result);
    }

    public async Task EditMessage(string messageId, string newContent)
    {
        var command = new EditMessageCommand(messageId, UserId, newContent);
        var result = await sender.Send(command);

        await Clients.Group($"chat_{result.Value.ChatId}")
            .SendAsync("MessageEdited", result);
    }

    public async Task DeleteMessage(string messageId)
    {
        var command = new DeleteMessageCommand(messageId, UserId);
        var result = await sender.Send(command);

        await Clients.Group($"chat_{result.Value.ChatId}")
            .SendAsync("MessageDeleted", new { MessageId = messageId, ChatId = result.Value.ChatId });
    }

    public async Task MarkMessagesAsRead(string chatId, List<string> messageIds)
    {
        var command = new MarkMessagesAsReadCommand(chatId, UserId, messageIds);
        await sender.Send(command);

        await Clients.OthersInGroup($"chat_{chatId}")
            .SendAsync("MessagesRead", new { ChatId = chatId, UserId, MessageIds = messageIds });
    }

    // ========================================
    // REACTIONS
    // ========================================

    public async Task AddReaction(string messageId, string emoji)
    {
        var command = new AddReactionCommand(messageId, UserId, emoji);
        var result = await sender.Send(command);

        await Clients.Group($"chat_{result.Value.ChatId}")
            .SendAsync("ReactionAdded", result);
    }

    public async Task RemoveReaction(string messageId, string emoji)
    {
        var command = new RemoveReactionCommand(messageId, UserId, emoji);
        var result = await sender.Send(command);

        await Clients.Group($"chat_{result.Value.ChatId}")
            .SendAsync("ReactionRemoved", result);
    }

    // ========================================
    // CHAT MANAGEMENT
    // ========================================

    public async Task CreateGroupChat(CreateGroupChatRequest request)
    {
        var command = new CreateGroupChatCommand(
            UserId,
            request.Name,
            request.Description,
            request.ParticipantIds);

        var result = await sender.Send(command);

        await Groups.AddToGroupAsync(Context.ConnectionId, $"chat_{result.Value.Id}");

        foreach (var participantId in request.ParticipantIds.Concat(new[] { UserId }))
        {
            await Clients.Group($"user_{participantId}")
                .SendAsync("ChatCreated", result);
        }
    }

    public async Task JoinChat(string chatId)
    {
        var isParticipant = await sender.Send(new IsUserParticipantQuery(chatId, UserId));
        if (!isParticipant.Value)
        {
            await Clients.Caller.SendAsync("Error", "Not authorized to join this chat");
            return;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, $"chat_{chatId}");

        await Clients.OthersInGroup($"chat_{chatId}")
            .SendAsync("UserJoinedChat", new { ChatId = chatId, UserId });
    }

    public async Task LeaveChat(string chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"chat_{chatId}");

        await Clients.OthersInGroup($"chat_{chatId}")
            .SendAsync("UserLeftChat", new { ChatId = chatId, UserId });
    }

    public async Task AddParticipant(string chatId, string participantId)
    {
        var command = new AddParticipantCommand(chatId, participantId, UserId);
        var result = await sender.Send(command);

        await Clients.Group($"chat_{chatId}")
            .SendAsync("ParticipantAdded", result);

        await Clients.Group($"user_{participantId}")
            .SendAsync("AddedToChat", result.Value.ChatResponse);
    }

    public async Task RemoveParticipant(string chatId, string participantId)
    {
        var command = new RemoveParticipantCommand(chatId, participantId, UserId);
        await sender.Send(command);

        await Clients.Group($"user_{participantId}")
            .SendAsync("RemovedFromChat", new { ChatId = chatId });

        await Clients.Group($"chat_{chatId}")
            .SendAsync("ParticipantRemoved", new { ChatId = chatId, ParticipantId = participantId });
    }

    // ========================================
    // TYPING INDICATORS
    // ========================================

    public async Task StartTyping(string chatId)
    {
        var isParticipant = await sender.Send(new IsUserParticipantQuery(chatId, UserId));
        if (!isParticipant.Value)
        {
            await Clients.Caller.SendAsync("Error", "Not authorized");
            return;
        }

        await Clients.OthersInGroup($"chat_{chatId}")
            .SendAsync("UserStartedTyping", new { ChatId = chatId, UserId });
    }

    public async Task StopTyping(string chatId)
    {
        await Clients.OthersInGroup($"chat_{chatId}")
            .SendAsync("UserStoppedTyping", new { ChatId = chatId, UserId });
    }
}