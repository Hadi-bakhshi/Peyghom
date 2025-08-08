using Peyghom.Common.Application.Messaging;

namespace Peyghom.Modules.Chat.Features.MarkMessagesAsRead;

public sealed record MarkMessagesAsReadCommand(string ChatId, string UserId, List<string> MessageIds) : ICommand;