using Mapster;
using Peyghom.Common.Application.Messaging;
using Peyghom.Common.Domain;
using Peyghom.Modules.Chat.Features.CreateGroupChat;
using Peyghom.Modules.Chat.Infrastructure.Repository.Chats;

namespace Peyghom.Modules.Chat.Features.GetUserChats;

public sealed class GetUserChatsQueryHandler :
    IQueryHandler<GetUserChatsQuery, List<ChatResponse>>
{
    private readonly IChatRepository _chatRepository;

    public GetUserChatsQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Result<List<ChatResponse>>> Handle(
        GetUserChatsQuery request, CancellationToken cancellationToken)
    {
        var chats = await _chatRepository.GetUserChatsAsync(request.UserId, cancellationToken);

        var mappedChats = chats.Select(item => item.Adapt<ChatResponse>()).ToList();

        return Result.Success(mappedChats);
    }
}
