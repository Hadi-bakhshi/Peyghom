using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Peyghom.Common.Infrastructure.Repository;
using Peyghom.Modules.Chat.Domain;

namespace Peyghom.Modules.Chat.Infrastructure.Repository.Messages;

internal sealed class MessageRepository : BaseRepository<Message>, IMessageRepository
{
    private readonly ILogger<MessageRepository> _logger;
    public MessageRepository(
        IMongoClient mongoClient,
        ILogger<MessageRepository> logger
        )
        : base(mongoClient, "messages")
    {
        _logger = logger;
    }

    public override async Task<Message?> GetByIdAsync(string id)
    {
        return await FindOneAsync(x => x.Id == id && !x.IsDeleted);
    }

    public override async Task<IEnumerable<Message>> GetAllAsync()
    {
        return await FindAsync(x => !x.IsDeleted);
    }

    public override async Task<Message> AddAsync(Message entity)
    {
        entity.Id = ObjectId.GenerateNewId().ToString();
        entity.Timestamp = DateTime.UtcNow;
        entity.IsDeleted = false;
        entity.IsEdited = false;

        return await base.AddAsync(entity);
    }

    public override async Task<bool> DeleteAsync(string id)
    {
        var message = await GetByIdAsync(id);
        if (message == null) return false;

        message.IsDeleted = true;
        message.DeletedAt = DateTime.UtcNow;
        await UpdateAsync(message);
        return true;

    }

    public override async Task<bool> ExistsAsync(string id)
    {
        var count = await CountAsync(x => x.Id == id && !x.IsDeleted);
        return count > 0;
    }

    public async Task<List<Message>> GetChatMessagesAsync(string chatId,
        int page = 1, int pageSize = 50, CancellationToken cancellationToken = default)
    {
        var skip = (page - 1) * pageSize;

        var filter = Builders<Message>.Filter.And(
            Builders<Message>.Filter.Eq(x => x.ChatId, chatId),
            Builders<Message>.Filter.Eq(x => x.IsDeleted, false)
        );

        return await _collection
            .Find(filter)
            .SortByDescending(x => x.Timestamp)
            .Skip(skip)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Message>> GetChatMessagesBeforeAsync(string chatId,
        DateTime before, int limit = 50, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Message>.Filter.And(
            Builders<Message>.Filter.Eq(x => x.ChatId, chatId),
            Builders<Message>.Filter.Lt(x => x.Timestamp, before),
            Builders<Message>.Filter.Eq(x => x.IsDeleted, false)
        );

        return await _collection
            .Find(filter)
            .SortByDescending(x => x.Timestamp)
            .Limit(limit)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Message>> GetChatMessagesAfterAsync(string chatId,
        DateTime after, int limit = 50, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Message>.Filter.And(
            Builders<Message>.Filter.Eq(x => x.ChatId, chatId),
            Builders<Message>.Filter.Gt(x => x.Timestamp, after),
            Builders<Message>.Filter.Eq(x => x.IsDeleted, false)
        );

        return await _collection
            .Find(filter)
            .SortBy(x => x.Timestamp)
            .Limit(limit)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Message>> GetUnreadMessagesAsync(string chatId,
        string userId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Message>.Filter.And(
            Builders<Message>.Filter.Eq(x => x.ChatId, chatId),
            Builders<Message>.Filter.Ne(x => x.SenderId, userId),
            Builders<Message>.Filter.Eq(x => x.IsDeleted, false),
            Builders<Message>.Filter.Not(
                Builders<Message>.Filter.ElemMatch(x => x.DeliveryStatus,
                    ds => ds.UserId == userId && ds.Status == DeliveryStatus.Read)
            )
        );

        return await _collection.Find(filter)
            .SortBy(x => x.Timestamp)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetUnreadMessagesCountAsync(string chatId,
        string userId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Message>.Filter.And(
            Builders<Message>.Filter.Eq(x => x.ChatId, chatId),
            Builders<Message>.Filter.Ne(x => x.SenderId, userId),
            Builders<Message>.Filter.Eq(x => x.IsDeleted, false),
            Builders<Message>.Filter.Not(
                Builders<Message>.Filter.ElemMatch(x => x.DeliveryStatus,
                    ds => ds.UserId == userId && ds.Status == DeliveryStatus.Read)
            )
        );

        return (int)await _collection
            .CountDocumentsAsync(filter, cancellationToken: cancellationToken);
    }

    public async Task<Message?> GetLastMessageAsync(string chatId,
        CancellationToken cancellationToken = default)
    {

        var filter = Builders<Message>.Filter.And(
            Builders<Message>.Filter.Eq(x => x.ChatId, chatId),
            Builders<Message>.Filter.Eq(x => x.IsDeleted, false)
        );

        return await _collection
            .Find(filter)
            .SortByDescending(x => x.Timestamp)
            .FirstOrDefaultAsync(cancellationToken);

    }

    public async Task AddReactionAsync(string messageId,
        MessageReaction reaction, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Message>.Filter.Eq(x => x.Id, messageId);
        var update = Builders<Message>.Update.Push(x => x.Reactions, reaction);

        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task RemoveReactionAsync(string messageId,
        string userId, string emoji, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Message>.Filter.Eq(x => x.Id, messageId);
        var update = Builders<Message>.Update.PullFilter(x => x.Reactions,
            r => r.UserId == userId && r.Emoji == emoji);

        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task UpdateDeliveryStatusAsync(string messageId,
        string userId, DeliveryStatus status, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Message>.Filter.And(
            Builders<Message>.Filter.Eq(x => x.Id, messageId),
            Builders<Message>.Filter.Not(
                Builders<Message>.Filter.ElemMatch(x => x.DeliveryStatus, ds => ds.UserId == userId)
            )
        );

        var deliveryStatus = new MessageDeliveryStatus
        {
            UserId = userId,
            Status = status,
            Timestamp = DateTime.UtcNow
        };

        var update = Builders<Message>.Update.Push(x => x.DeliveryStatus, deliveryStatus);
        var result = await _collection
            .UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

        if (result.ModifiedCount == 0)
        {
            var updateFilter = Builders<Message>.Filter.And(
                Builders<Message>.Filter.Eq(x => x.Id, messageId),
                Builders<Message>.Filter.ElemMatch(x => x.DeliveryStatus, ds => ds.UserId == userId)
            );

            var updateExisting = Builders<Message>.Update
                .Set("deliveryStatus.$.status", status)
                .Set("deliveryStatus.$.timestamp", DateTime.UtcNow);

            await _collection
                .UpdateOneAsync(updateFilter, updateExisting, cancellationToken: cancellationToken);
        }
    }

    public async Task MarkMessageAsEditedAsync(string messageId,
        string newContent, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Message>.Filter.Eq(x => x.Id, messageId);
        var update = Builders<Message>.Update
            .Set(x => x.Content, newContent)
            .Set(x => x.IsEdited, true)
            .Set(x => x.EditedAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    protected override string GetEntityId(Message entity) => entity.Id;

    public async Task MarkMessageAsDeletedAsync(string messageId,
        CancellationToken cancellationToken = default)
    {
        var filter = Builders<Message>.Filter.Eq(x => x.Id, messageId);
        var update = Builders<Message>.Update
            .Set(x => x.IsDeleted, true)
            .Set(x => x.DeletedAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task<List<Message>> SearchMessagesAsync(string chatId,
        string searchTerm, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Message>.Filter.And(
            Builders<Message>.Filter.Eq(x => x.ChatId, chatId),
            Builders<Message>.Filter.Eq(x => x.IsDeleted, false),
            Builders<Message>.Filter.Regex(x => x.Content, new BsonRegularExpression(searchTerm, "i"))
        );

        return await _collection
            .Find(filter)
            .SortByDescending(x => x.Timestamp)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Message>> GetMessagesByTypeAsync(string chatId,
        MessageType messageType, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Message>.Filter.And(
            Builders<Message>.Filter.Eq(x => x.ChatId, chatId),
            Builders<Message>.Filter.Eq(x => x.MessageType, messageType),
            Builders<Message>.Filter.Eq(x => x.IsDeleted, false)
        );

        return await _collection
            .Find(filter)
            .SortByDescending(x => x.Timestamp)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Message>> GetRepliesAsync(string originalMessageId,
        CancellationToken cancellationToken = default)
    {
        var filter = Builders<Message>.Filter.And(
            Builders<Message>.Filter.Eq(x => x.ReplyToMessageId, originalMessageId),
            Builders<Message>.Filter.Eq(x => x.IsDeleted, false)
        );

        return await _collection
            .Find(filter)
            .SortBy(x => x.Timestamp)
            .ToListAsync(cancellationToken);
    }

    public async Task BulkUpdateDeliveryStatusAsync(List<string> messageIds,
        string userId, DeliveryStatus status, CancellationToken cancellationToken = default)
    {
        var deliveryStatus = new MessageDeliveryStatus
        {
            UserId = userId,
            Status = status,
            Timestamp = DateTime.UtcNow
        };

        // First, try to add delivery status for messages where user doesn't have a status yet
        var filterForNew = Builders<Message>.Filter.And(
            Builders<Message>.Filter.In(x => x.Id, messageIds),
            Builders<Message>.Filter.Not(
                Builders<Message>.Filter.ElemMatch(x => x.DeliveryStatus, ds => ds.UserId == userId)
            )
        );

        var updateForNew = Builders<Message>.Update.Push(x => x.DeliveryStatus, deliveryStatus);
        await _collection.UpdateManyAsync(filterForNew, updateForNew, cancellationToken: cancellationToken);

        // Then, update existing delivery status for messages where user already has a status
        var filterForExisting = Builders<Message>.Filter.And(
            Builders<Message>.Filter.In(x => x.Id, messageIds),
            Builders<Message>.Filter.ElemMatch(x => x.DeliveryStatus, ds => ds.UserId == userId)
        );

        var updateForExisting = Builders<Message>.Update
            .Set("deliveryStatus.$.status", status)
            .Set("deliveryStatus.$.timestamp", DateTime.UtcNow);

        // Note: MongoDB's positional operator ($) only works with single document updates
        // For bulk operations with positional updates, we need to process each message individually
        var existingMessages = await _collection
            .Find(filterForExisting)
            .Project(x => x.Id)
            .ToListAsync(cancellationToken);

        var bulkOps = new List<WriteModel<Message>>();
        foreach (var messageId in existingMessages)
        {
            var singleFilter = Builders<Message>.Filter.And(
                Builders<Message>.Filter.Eq(x => x.Id, messageId),
                Builders<Message>.Filter.ElemMatch(x => x.DeliveryStatus, ds => ds.UserId == userId)
            );

            var singleUpdate = Builders<Message>.Update
                .Set("deliveryStatus.$.status", status)
                .Set("deliveryStatus.$.timestamp", DateTime.UtcNow);

            bulkOps.Add(new UpdateOneModel<Message>(singleFilter, singleUpdate));
        }

        if (bulkOps.Count != 0)
        {
            await _collection.BulkWriteAsync(bulkOps, cancellationToken: cancellationToken);
        }
    }
}
