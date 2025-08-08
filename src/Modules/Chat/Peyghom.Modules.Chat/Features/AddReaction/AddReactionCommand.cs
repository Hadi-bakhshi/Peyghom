using Peyghom.Common.Application.Messaging;

namespace Peyghom.Modules.Chat.Features.AddReaction;

public sealed record AddReactionCommand(string MessageId, string UserId, string Emoji) : ICommand<ReactionResponse>;