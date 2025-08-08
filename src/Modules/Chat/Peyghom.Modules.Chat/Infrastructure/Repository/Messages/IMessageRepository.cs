using Peyghom.Common.Application.Repository;
using Peyghom.Modules.Chat.Domain;

namespace Peyghom.Modules.Chat.Infrastructure.Repository.Messages;

public interface IMessageRepository : IRepository<Message>
{
    Task<List<Message>> GetChatMessagesAsync(string chatId, int page = 1, int pageSize = 50, CancellationToken cancellationToken = default);
    Task<List<Message>> GetChatMessagesBeforeAsync(string chatId, DateTime before, int limit = 50, CancellationToken cancellationToken = default);
    Task<List<Message>> GetChatMessagesAfterAsync(string chatId, DateTime after, int limit = 50, CancellationToken cancellationToken = default);
    Task<List<Message>> GetUnreadMessagesAsync(string chatId, string userId, CancellationToken cancellationToken = default);
    Task<int> GetUnreadMessagesCountAsync(string chatId, string userId, CancellationToken cancellationToken = default);
    Task<Message?> GetLastMessageAsync(string chatId, CancellationToken cancellationToken = default);
    Task AddReactionAsync(string messageId, MessageReaction reaction, CancellationToken cancellationToken = default);
    Task RemoveReactionAsync(string messageId, string userId, string emoji, CancellationToken cancellationToken = default);
    Task UpdateDeliveryStatusAsync(string messageId, string userId, DeliveryStatus status, CancellationToken cancellationToken = default);
    Task MarkMessageAsEditedAsync(string messageId, string newContent, CancellationToken cancellationToken = default);
    Task MarkMessageAsDeletedAsync(string messageId, CancellationToken cancellationToken = default);
    Task<List<Message>> SearchMessagesAsync(string chatId, string searchTerm, CancellationToken cancellationToken = default);
    Task<List<Message>> GetMessagesByTypeAsync(string chatId, MessageType messageType, CancellationToken cancellationToken = default);
    Task<List<Message>> GetRepliesAsync(string originalMessageId, CancellationToken cancellationToken = default);
    Task BulkUpdateDeliveryStatusAsync(List<string> messageIds, string userId, DeliveryStatus status, CancellationToken cancellationToken = default);

}
