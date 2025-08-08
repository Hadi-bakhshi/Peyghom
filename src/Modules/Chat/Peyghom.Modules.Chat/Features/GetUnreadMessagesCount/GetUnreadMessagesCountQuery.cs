using Peyghom.Common.Application.Messaging;

namespace Peyghom.Modules.Chat.Features.GetUnreadMessagesCount;

public sealed record GetUnreadMessagesCountQuery(string ChatId, string UserId) : IQuery<int>;