using Peyghom.Common.Application.Messaging;
using Peyghom.Modules.Chat.Features.SendMessage;

namespace Peyghom.Modules.Chat.Features.EditMessage;

public sealed record EditMessageCommand(
    string MessageId,
    string UserId,
    string NewContent) : ICommand<MessageResponse>;
