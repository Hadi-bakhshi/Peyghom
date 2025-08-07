using Peyghom.Modules.Chat.Contracts;
using Peyghom.Modules.Chat.Domain;

namespace Peyghom.Modules.Chat.Features.SendMessage;

public sealed record MessageResponse(string Id,
    string ChatId,
    string SenderId,
    string Content,
    MessageType MessageType,
    DateTime Timestamp,
    bool IsEdited,
    DateTime? EditedAt,
    string? ReplyToMessageId,
    List<MessageAttachmentRequest>? Attachments,
    List<MessageReactionResponse>? Reactions);
