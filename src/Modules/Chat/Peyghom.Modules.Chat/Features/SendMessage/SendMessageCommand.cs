using Peyghom.Common.Application.Messaging;
using Peyghom.Modules.Chat.Domain;
using Peyghom.Modules.Chat.Hubs;

namespace Peyghom.Modules.Chat.Features.SendMessage;

public sealed record SendMessageCommand(
 string ChatId,
 string SenderId,
 string Content,
 MessageType MessageType,
 string? ReplyToMessageId,
 List<MessageAttachmentRequest>? Attachments) : ICommand<MessageResponse>;


