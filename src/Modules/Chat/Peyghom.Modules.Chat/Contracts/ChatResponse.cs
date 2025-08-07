using Peyghom.Modules.Chat.Domain;

namespace Peyghom.Modules.Chat.Contracts;

public sealed record ChatResponse(
    string Id,
    string? Name,
    string? Description,
    ChatType Type,
    List<ChatParticipantResponse> Participants,
    string CreatedBy,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    LastMessageResponse? LastMessage,
    string? GroupImage);

