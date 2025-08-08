using Peyghom.Common.Domain;

namespace Peyghom.Modules.Chat.Domain;

public static class MessageErrors
{
    public static readonly Error ReplyToMessageNotFound = Error.NotFound(
        "Message.ReplyToMessageNotFound",
        "Sorry, couldn't find the message you're replying to");
}