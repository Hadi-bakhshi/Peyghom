using MongoDB.Driver;
using Peyghom.Modules.Users.Domain;
using Peyghom.Common.Infrastructure.Repository;


namespace Peyghom.Modules.Users.Infrastructure.Repository.UserNotifications;

internal sealed class UserNotificationRepository : BaseRepository<UserNotification>, IUserNotificationRepository
{
    public UserNotificationRepository(IMongoClient mongoClient)
        : base(mongoClient, "notifications")
    {
        CreateIndexes();
    }

    public async Task<IEnumerable<UserNotification>> GetUserNotificationsAsync(string userId)
    {
        return await _collection
            .Find(n => n.UserId == userId)
            .SortByDescending(n => n.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserNotification>> GetUnreadNotificationsAsync(string userId)
    {
        return await FindAsync(n => n.UserId == userId && !n.IsRead);
    }

    public async Task<long> GetUnreadCountAsync(string userId)
    {
        return await CountAsync(n => n.UserId == userId && !n.IsRead);
    }

    public async Task MarkAsReadAsync(string notificationId)
    {
        var filter = Builders<UserNotification>.Filter.Eq(n => n.Id, notificationId);
        var update = Builders<UserNotification>.Update
            .Set(n => n.IsRead, true)
            .Set(n => n.ReadAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(filter, update);
    }

    public async Task MarkAllAsReadAsync(string userId)
    {
        var filter = Builders<UserNotification>.Filter.And(
            Builders<UserNotification>.Filter.Eq(n => n.UserId, userId),
            Builders<UserNotification>.Filter.Eq(n => n.IsRead, false)
        );

        var update = Builders<UserNotification>.Update
            .Set(n => n.IsRead, true)
            .Set(n => n.ReadAt, DateTime.UtcNow);

        await _collection.UpdateManyAsync(filter, update);
    }

    public async Task<IEnumerable<UserNotification>> GetNotificationsByTypeAsync(string userId, NotificationType type)
    {
        return await FindAsync(n => n.UserId == userId && n.Type == type);
    }

    public async Task DeleteOldNotificationsAsync(DateTime cutoffDate)
    {
        var filter = Builders<UserNotification>.Filter.Lt(n => n.CreatedAt, cutoffDate);
        await _collection.DeleteManyAsync(filter);
    }

    protected override string GetEntityId(UserNotification entity) => entity.Id;

    private void CreateIndexes()
    {
        var indexModels = new[]
        {
            new CreateIndexModel<UserNotification>(
                Builders<UserNotification>.IndexKeys.Ascending(n => n.UserId)),
            new CreateIndexModel<UserNotification>(
                Builders<UserNotification>.IndexKeys.Ascending(n => n.IsRead)),
            new CreateIndexModel<UserNotification>(
                Builders<UserNotification>.IndexKeys.Descending(n => n.CreatedAt)),
            new CreateIndexModel<UserNotification>(
                Builders<UserNotification>.IndexKeys.Combine(
                    Builders<UserNotification>.IndexKeys.Ascending(n => n.UserId),
                    Builders<UserNotification>.IndexKeys.Ascending(n => n.IsRead)))
        };

        _collection.Indexes.CreateMany(indexModels);
    }
}
