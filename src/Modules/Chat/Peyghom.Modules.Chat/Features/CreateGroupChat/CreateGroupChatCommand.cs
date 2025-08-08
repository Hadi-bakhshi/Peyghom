using Peyghom.Common.Application.Messaging;

namespace Peyghom.Modules.Chat.Features.CreateGroupChat;

public sealed record CreateGroupChatCommand(string CreatorId,
    string Name,
    string? Description,
    List<string> ParticipantIds) : ICommand<ChatResponse>;