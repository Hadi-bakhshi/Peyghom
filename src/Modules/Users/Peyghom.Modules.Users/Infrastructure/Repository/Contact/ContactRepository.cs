using MongoDB.Driver;
using Peyghom.Common.Infrastructure.Repository;
using Peyghom.Modules.Users.Domain;

namespace Peyghom.Modules.Users.Infrastructure.Repository.Contacts;

internal sealed class ContactRepository : BaseRepository<Contact>, IContactRepository
{
    public ContactRepository(IMongoClient mongoClient)
        : base(mongoClient, "contacts")
    {
        CreateIndexes();
    }

    public async Task<IEnumerable<Contact>> GetUserContactsAsync(string userId)
    {
        return await FindAsync(c => c.UserId == userId && c.Status == ContactStatus.Accepted);
    }

    public async Task<IEnumerable<Contact>> GetContactRequestsAsync(string userId)
    {
        return await FindAsync(c => c.ContactUserId == userId && c.Status == ContactStatus.Pending);
    }

    public async Task<IEnumerable<Contact>> GetFavoriteContactsAsync(string userId)
    {
        return await FindAsync(c => c.UserId == userId && c.IsFavorite);
    }

    public async Task<IEnumerable<Contact>> GetBlockedContactsAsync(string userId)
    {
        return await FindAsync(c => c.UserId == userId && c.IsBlocked);
    }

    public async Task<Contact?> GetContactAsync(string userId, string contactUserId)
    {
        return await FindOneAsync(c => c.UserId == userId && c.ContactUserId == contactUserId);
    }

    public async Task<bool> AreUsersConnectedAsync(string userId1, string userId2)
    {
        var contact1 = await GetContactAsync(userId1, userId2);
        var contact2 = await GetContactAsync(userId2, userId1);

        return contact1?.Status == ContactStatus.Accepted &&
               contact2?.Status == ContactStatus.Accepted;
    }

    public async Task UpdateStatusAsync(string contactId, ContactStatus status)
    {
        var filter = Builders<Contact>.Filter.Eq(c => c.Id, contactId);
        var update = Builders<Contact>.Update.Set(c => c.Status, status);

        await _collection.UpdateOneAsync(filter, update);
    }

    public async Task BlockContactAsync(string userId, string contactUserId)
    {
        var filter = Builders<Contact>.Filter.And(
            Builders<Contact>.Filter.Eq(c => c.UserId, userId),
            Builders<Contact>.Filter.Eq(c => c.ContactUserId, contactUserId)
        );

        var update = Builders<Contact>.Update
            .Set(c => c.IsBlocked, true)
            .Set(c => c.Status, ContactStatus.Blocked);

        await _collection.UpdateOneAsync(filter, update);
    }

    public async Task UnblockContactAsync(string userId, string contactUserId)
    {
        var filter = Builders<Contact>.Filter.And(
            Builders<Contact>.Filter.Eq(c => c.UserId, userId),
            Builders<Contact>.Filter.Eq(c => c.ContactUserId, contactUserId)
        );

        var update = Builders<Contact>.Update
            .Set(c => c.IsBlocked, false);

        await _collection.UpdateOneAsync(filter, update);
    }

    public async Task SetFavoriteAsync(string contactId, bool isFavorite)
    {
        var filter = Builders<Contact>.Filter.Eq(c => c.Id, contactId);
        var update = Builders<Contact>.Update.Set(c => c.IsFavorite, isFavorite);

        await _collection.UpdateOneAsync(filter, update);
    }

    protected override string GetEntityId(Contact entity) => entity.Id;

    private void CreateIndexes()
    {
        var indexModels = new[]
        {
            new CreateIndexModel<Contact>(
                Builders<Contact>.IndexKeys.Ascending(c => c.UserId)),
            new CreateIndexModel<Contact>(
                Builders<Contact>.IndexKeys.Ascending(c => c.ContactUserId)),
            new CreateIndexModel<Contact>(
                Builders<Contact>.IndexKeys.Combine(
                    Builders<Contact>.IndexKeys.Ascending(c => c.UserId),
                    Builders<Contact>.IndexKeys.Ascending(c => c.ContactUserId)),
                new CreateIndexOptions { Unique = true }),
            new CreateIndexModel<Contact>(
                Builders<Contact>.IndexKeys.Ascending(c => c.Status))
        };

        _collection.Indexes.CreateMany(indexModels);
    }
}
