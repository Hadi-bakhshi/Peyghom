using Peyghom.Common.Domain;

namespace Peyghom.Modules.Chat.Domain;

public static class ChatErrors
{
    public static readonly Error InvalidChatName = Error.Problem(
        "Chat.InvalidChatName",
        "Chat name is invalid");
    
    public static readonly Error NoParticipants = Error.Problem(
        "Chat.NoParticipants",
        "Participant list is empty");
    
    public static readonly Error UserNotParticipant = Error.Problem(
        "Chat.UserNotParticipant",
        "Sorry, the requested chat does not belong you");
}