using Peyghom.Modules.Chat.Features.CreateGroupChat;

namespace Peyghom.Modules.Chat.Features.AddParticipant;

public sealed record AddParticipantResponse(ChatResponse ChatResponse, ChatParticipantResponse ChatParticipantResponse);