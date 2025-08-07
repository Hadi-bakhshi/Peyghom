namespace Peyghom.Modules.Chat.Contracts;

public sealed record MessageReactionResponse(
    string UserId,
    string Emoji,
    DateTime Timestamp);
