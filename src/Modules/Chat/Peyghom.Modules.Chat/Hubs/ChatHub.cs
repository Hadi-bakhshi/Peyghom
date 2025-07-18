using Microsoft.AspNetCore.SignalR;
using MediatR;
using Microsoft.Extensions.Logging;
using Peyghom.Modules.Chat.Features.SendMessage;

namespace Peyghom.Modules.Chat.Hubs;

public class ChatHub(ISender sender, ILogger<ChatHub> _logger) : Hub 
{
    public async Task JoinChat(string chatId)
    {
        var userId = Context.UserIdentifier;
        await Groups.AddToGroupAsync(Context.ConnectionId, $"chat_{chatId}");

        //var command = new JoinChatCommand { ChatId = chatId, UserId = userId };
        var command = new { ChatId = chatId, UserId = userId };
        await sender.Send(command);

        await Clients.Group($"chat_{chatId}").SendAsync("UserJoined", userId);
        _logger.LogInformation("User {UserId} joined chat {ChatId}", userId, chatId);
    }

    public async Task LeaveChat(string chatId)
    {
        var userId = Context.UserIdentifier;
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"chat_{chatId}");

        //var command = new LeaveChatCommand { ChatId = chatId, UserId = userId };
        var command = new { ChatId = chatId, UserId = userId };
        await sender.Send(command);

        await Clients.Group($"chat_{chatId}").SendAsync("UserLeft", userId);
        _logger.LogInformation("User {UserId} left chat {ChatId}", userId, chatId);
    }

    public async Task SendMessage(SendMessageRequest request)
    {
        var userId = Context.UserIdentifier;
        var command = new SendMessageCommand(
            request.ChatId,
            userId,
            request.Content,
            request.MessageType,
            request.ReplyToMessageId,
            request.Attachments);
      
        var result = await sender.Send(command);

        if (result.IsSuccess)
        {
            await Clients.Group($"chat_{request.ChatId}").SendAsync("MessageReceived", result.Value);

            // Send delivery confirmations
            //var deliveryCommand = new MarkMessageDeliveredCommand
            var deliveryCommand = new
            {
                MessageId = result.Value.Id,
                ChatId = request.ChatId
            };
            await sender.Send(deliveryCommand);
        }
    }

    public async Task MarkMessageAsRead(string messageId, string chatId)
    {
        var userId = Context.UserIdentifier;
        //var command = new MarkMessageAsReadCommand
        var command = new
        {
            MessageId = messageId,
            ChatId = chatId,
            UserId = userId
        };

        await sender.Send(command);
        await Clients.Group($"chat_{chatId}").SendAsync("MessageRead", messageId, userId);
    }

    public async Task StartTyping(string chatId)
    {
        var userId = Context.UserIdentifier;
        //var command = new StartTypingCommand { ChatId = chatId, UserId = userId };
        var command = new { ChatId = chatId, UserId = userId };
        await sender.Send(command);

        await Clients.GroupExcept($"chat_{chatId}", Context.ConnectionId)
            .SendAsync("UserStartedTyping", userId);
    }

    public async Task StopTyping(string chatId)
    {
        var userId = Context.UserIdentifier;
        //var command = new StopTypingCommand { ChatId = chatId, UserId = userId };
        var command = new { ChatId = chatId, UserId = userId };
        await sender.Send(command);

        await Clients.GroupExcept($"chat_{chatId}", Context.ConnectionId)
            .SendAsync("UserStoppedTyping", userId);
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        _logger.LogInformation("User {UserId} connected to chat hub", userId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.UserIdentifier;
        _logger.LogInformation("User {UserId} disconnected from chat hub", userId);
        await base.OnDisconnectedAsync(exception);
    }
}



