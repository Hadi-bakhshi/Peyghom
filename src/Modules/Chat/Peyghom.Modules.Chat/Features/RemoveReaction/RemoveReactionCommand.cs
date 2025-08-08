using Peyghom.Common.Application.Messaging;
using Peyghom.Modules.Chat.Features.AddReaction;

namespace Peyghom.Modules.Chat.Features.RemoveReaction;

public sealed record RemoveReactionCommand(string MessageId, string UserId, string Emoji) : ICommand<ReactionResponse>;