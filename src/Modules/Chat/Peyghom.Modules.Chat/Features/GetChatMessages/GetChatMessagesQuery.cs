using Peyghom.Common.Application.Messaging;
using Peyghom.Modules.Chat.Features.SendMessage;

namespace Peyghom.Modules.Chat.Features.GetChatMessages;

public sealed record GetChatMessagesQuery(
    string ChatId,
    string UserId,
    int Page = 1,
    int PageSize = 50) : IQuery<List<MessageResponse>>;