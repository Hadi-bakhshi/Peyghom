using MongoDB.Driver;
using Peyghom.Common.Infrastructure.Repository;
using Peyghom.Modules.Users.Domain;

namespace Peyghom.Modules.Users.Infrastructure.Repository.Users;

internal sealed class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IMongoClient mongoClient)
        : base(mongoClient, "users")
    {
        CreateIndexes();
    }

    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await FindOneAsync(u => u.Username == username);
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await FindOneAsync(u => u.Email == email);
    }

    public async Task<User?> FindByPhoneNumberAsync(string phoneNumber)
    {
        return await FindOneAsync(u => u.PhoneNumber == phoneNumber);
    }

    public async Task<IEnumerable<User>> FindByStatusAsync(UserStatus status)
    {
        return await FindAsync(u => u.Status == status);
    }

    public async Task<IEnumerable<User>> FindOnlineUsersAsync()
    {
        return await FindAsync(u => u.IsOnline);
    }

    public async Task<IEnumerable<User>> FindByRoleAsync(string roleName)
    {
        return await FindAsync(u => u.RoleNames.Contains(roleName));
    }

    public async Task<bool> IsUsernameAvailableAsync(string username)
    {
        var user = await FindByUsernameAsync(username);
        return user == null;
    }

    public async Task<bool> IsEmailAvailableAsync(string email)
    {
        var user = await FindByEmailAsync(email);
        return user == null;
    }

    public async Task UpdateLastSeenAsync(string userId, DateTime lastSeen)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
        var update = Builders<User>.Update
            .Set(u => u.LastSeen, lastSeen)
            .Set(u => u.UpdatedAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(filter, update);
    }

    public async Task UpdateOnlineStatusAsync(string userId, bool isOnline)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
        var update = Builders<User>.Update
            .Set(u => u.IsOnline, isOnline)
            .Set(u => u.UpdatedAt, DateTime.UtcNow);

        if (!isOnline)
        {
            update = update.Set(u => u.LastSeen, DateTime.UtcNow);
        }

        await _collection.UpdateOneAsync(filter, update);
    }

    protected override string GetEntityId(User entity) => entity.Id;

    private void CreateIndexes()
    {
        var indexModels = new[]
        {
            new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(u => u.Username),
                new CreateIndexOptions { Unique = true }),
            new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(u => u.Email),
                new CreateIndexOptions { Unique = true, Sparse = true }),
            new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(u => u.PhoneNumber),
                new CreateIndexOptions { Unique = true, Sparse = true }),
            new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(u => u.Status)),
            new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(u => u.IsOnline))
        };

        _collection.Indexes.CreateMany(indexModels);
    }
}
