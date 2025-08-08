using Peyghom.Common.Application.Messaging;

namespace Peyghom.Modules.Chat.Features.IsUserParticipant;

public sealed record IsUserParticipantQuery(string ChatId, string UserId) : IQuery<bool>;