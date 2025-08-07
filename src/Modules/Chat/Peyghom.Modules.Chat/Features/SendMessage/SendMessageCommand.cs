using Peyghom.Common.Application.Messaging;
using Peyghom.Modules.Chat.Domain;
using Peyghom.Modules.Chat.Hubs;

namespace Peyghom.Modules.Chat.Features.SendMessage;

public sealed record SendMessageCommand(
    string SenderId,
    string ChatId,
    string Content,
    MessageType MessageType = MessageType.Text,
    string? ReplyToMessageId = null,
    List<MessageAttachmentRequest>? Attachments = null) : ICommand<MessageResponse>;


