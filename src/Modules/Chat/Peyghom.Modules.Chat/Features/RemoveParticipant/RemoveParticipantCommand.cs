using Peyghom.Common.Application.Messaging;

namespace Peyghom.Modules.Chat.Features.RemoveParticipant;

public sealed record RemoveParticipantCommand(string ChatId, string ParticipantId, string RequesterId) : ICommand;