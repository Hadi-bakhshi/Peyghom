using Peyghom.Modules.Chat.Domain;
using Peyghom.Modules.Chat.Features.AddReaction;

namespace Peyghom.Modules.Chat.Features.SendMessage;

public sealed record MessageResponse(
    string Id,
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
