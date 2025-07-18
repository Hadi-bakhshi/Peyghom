using Peyghom.Modules.Chat.Domain;
using Peyghom.Modules.Chat.Hubs;

namespace Peyghom.Modules.Chat.Features.SendMessage;

public sealed record SendMessageRequest(
    string ChatId,
    string Content,
    MessageType MessageType,
    string ReplyToMessageId,
    List<MessageAttachmentRequest>? Attachments);

