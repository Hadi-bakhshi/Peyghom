using Peyghom.Common.Application.Messaging;
using Peyghom.Modules.Chat.Features.SendMessage;

namespace Peyghom.Modules.Chat.Features.SearchMessages;

public sealed record SearchMessagesQuery(
    string ChatId,
    string UserId,
    string SearchTerm) :IQuery<List<MessageResponse>>; 