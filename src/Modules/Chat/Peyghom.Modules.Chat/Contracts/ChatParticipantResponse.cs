using Peyghom.Modules.Chat.Domain;

namespace Peyghom.Modules.Chat.Contracts;

public sealed record ChatParticipantResponse(
    string UserId,
    ParticipantRole Role,
    DateTime JoinedAt,
    string? LastReadMessageId,
    bool IsMuted,
    bool IsBlocked);

