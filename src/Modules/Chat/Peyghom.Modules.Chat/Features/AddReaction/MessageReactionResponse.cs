namespace Peyghom.Modules.Chat.Features.AddReaction;

public sealed record MessageReactionResponse(string UserId, string Emoji, DateTime Timestamp);