using MongoDB.Driver;
using Peyghom.Common.Infrastructure.Repository;
using Peyghom.Modules.Chat.Domain;

namespace Peyghom.Modules.Chat.Infrastructure.Repository.Messages;

internal sealed class MessageRepository : BaseRepository<Message>, IMessageRepository
{
    public MessageRepository(IMongoClient mongoClient, string collectionName) : base(mongoClient, collectionName)
    {
    }

    public Task AddReactionAsync(string messageId, MessageReaction reaction, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task BulkUpdateDeliveryStatusAsync(List<string> messageIds, string userId, DeliveryStatus status, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<string> CreateAsync(Message message, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Message?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Message>> GetByIdsAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Message>> GetChatMessagesAfterAsync(string chatId, DateTime after, int limit = 50, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Message>> GetChatMessagesAsync(string chatId, int page = 1, int pageSize = 50, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Message>> GetChatMessagesBeforeAsync(string chatId, DateTime before, int limit = 50, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Message?> GetLastMessageAsync(string chatId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Message>> GetMessagesByTypeAsync(string chatId, MessageType messageType, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Message>> GetRepliesAsync(string originalMessageId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Message>> GetUnreadMessagesAsync(string chatId, string userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetUnreadMessagesCountAsync(string chatId, string userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task MarkMessageAsDeletedAsync(string messageId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task MarkMessageAsEditedAsync(string messageId, string newContent, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveReactionAsync(string messageId, string userId, string emoji, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Message>> SearchMessagesAsync(string chatId, string searchTerm, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Message message, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateDeliveryStatusAsync(string messageId, string userId, DeliveryStatus status, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    protected override string GetEntityId(Message entity)
    {
        throw new NotImplementedException();
    }
}
