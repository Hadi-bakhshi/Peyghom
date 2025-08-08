using Peyghom.Common.Application.Messaging;

namespace Peyghom.Modules.Chat.Features.DeleteMessage;

public sealed record DeleteMessageCommand(
    string MessageId,
    string UserId) : ICommand<DeleteMessageResponse>;