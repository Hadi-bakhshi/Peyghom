using Peyghom.Common.Application.Messaging;
using Peyghom.Modules.Chat.Features.CreateGroupChat;

namespace Peyghom.Modules.Chat.Features.GetUserChats;

public sealed record GetUserChatsQuery(string UserId) : IQuery<List<ChatResponse>>;