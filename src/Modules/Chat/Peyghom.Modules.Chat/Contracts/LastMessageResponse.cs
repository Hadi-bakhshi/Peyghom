using Peyghom.Modules.Chat.Domain;

namespace Peyghom.Modules.Chat.Contracts;

public sealed record LastMessageResponse(
    string MessageId,
    string Content,
    string SenderId,
    DateTime Timestamp,
    MessageType MessageType);

