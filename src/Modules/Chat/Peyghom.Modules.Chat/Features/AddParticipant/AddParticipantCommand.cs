using Peyghom.Common.Application.Messaging;

namespace Peyghom.Modules.Chat.Features.AddParticipant;

public sealed record AddParticipantCommand(string ChatId, string ParticipantId, string RequesterId)
    : ICommand<AddParticipantResponse>;