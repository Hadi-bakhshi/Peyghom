using Peyghom.Modules.Chat.Domain;

namespace Peyghom.Modules.Chat.Features.SendMessage;

public sealed record MessageResponse(
  string Id,
  string ChatId,
  string SenderId,
  string Content,
  MessageType MessageType,
  DateTime Timestamp,
  string? ReplyToMessageId,
  List<MessageAttachment>? Attachments,
  bool IsEdited,
  bool IsDeleted);

