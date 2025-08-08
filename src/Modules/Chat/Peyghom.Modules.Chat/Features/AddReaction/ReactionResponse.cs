namespace Peyghom.Modules.Chat.Features.AddReaction;

public sealed record ReactionResponse(string MessageId, string ChatId, MessageReactionResponse Reaction);