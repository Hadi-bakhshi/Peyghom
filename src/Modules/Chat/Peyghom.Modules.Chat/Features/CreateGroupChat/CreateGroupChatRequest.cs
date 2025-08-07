namespace Peyghom.Modules.Chat.Features.CreateGroupChat;

public sealed record CreateGroupChatRequest(
    string Name,
    List<string> ParticipantIds,
    string? Description);
