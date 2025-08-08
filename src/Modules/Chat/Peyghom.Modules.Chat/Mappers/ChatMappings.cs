using Peyghom.Modules.Chat.Domain;
using Peyghom.Modules.Chat.Features.AddReaction;
using Peyghom.Modules.Chat.Features.CreateGroupChat;
using Peyghom.Modules.Chat.Features.SendMessage;

namespace Peyghom.Modules.Chat.Mappers;

internal static class ChatMappings
{
    public static ChatResponse ToChatResponse(this Domain.Chat chat)
    {
        return new ChatResponse(
            chat.Id,
            chat.Name,
            chat.Description,
            chat.Type,
            chat.Participants?.Select(ToParticipantResponse).ToList() ?? new List<ChatParticipantResponse>(),
            chat.CreatedBy,
            chat.CreatedAt,
            chat.UpdatedAt,
            chat.LastMessage?.ToLastMessageResponse(),
            chat.GroupImage);
    }

    public static ChatParticipantResponse ToParticipantResponse(this ChatParticipant participant)
    {
        return new ChatParticipantResponse(
            participant.UserId,
            participant.Role,
            participant.JoinedAt,
            participant.LastReadMessageId,
            participant.IsMuted,
            participant.IsBlocked);
    }

    public static LastMessageResponse ToLastMessageResponse(this LastMessageInfo last)
    {
        return new LastMessageResponse(
            last.MessageId,
            last.Content,
            last.SenderId,
            last.Timestamp,
            last.MessageType);
    }

    public static MessageResponse ToMessageResponse(this Message message)
    {
        var attachments = message.Attachments?.Select(a => new MessageAttachmentRequest(
            a.FileName,
            a.FileSize,
            a.FileType,
            a.FileUrl,
            a.ThumbnailUrl)).ToList();

        var reactions = message.Reactions?.Select(r => new MessageReactionResponse(
            r.UserId,
            r.Emoji,
            r.Timestamp)).ToList();

        return new MessageResponse(
            message.Id,
            message.ChatId,
            message.SenderId,
            message.Content,
            message.MessageType,
            message.Timestamp,
            message.IsEdited,
            message.EditedAt,
            message.ReplyToMessageId,
            attachments,
            reactions ?? new List<MessageReactionResponse>());
    }
}

