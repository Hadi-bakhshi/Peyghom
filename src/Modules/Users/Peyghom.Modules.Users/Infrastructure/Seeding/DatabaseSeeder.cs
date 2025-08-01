using MongoDB.Driver;
using Peyghom.Modules.Users.Domain;

namespace Peyghom.Modules.Users.Infrastructure.Seeding;

public sealed class DatabaseSeeder
{
    private readonly IMongoCollection<Role> _roleCollection;
    private readonly IMongoCollection<Permission> _permissionCollection;

    public DatabaseSeeder(IMongoClient mongoClient)
    {
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase("peyghom");
        _roleCollection = mongoDatabase.GetCollection<Role>("role");
        _permissionCollection = mongoDatabase.GetCollection<Permission>("permission");
    }


    public async Task SeedAsync()
    {
        await SeedPermissions();
        await SeedRoles();
    }

    private async Task SeedPermissions()
    {
        var allPermissions = Permission.GetAllPermissions().ToList();



        foreach (var permission in allPermissions)
        {
            var filter = Builders<Permission>.Filter.Eq(p => p.Code, permission.Code);

            await _permissionCollection.ReplaceOneAsync(
                filter,
                permission,
                new ReplaceOptions { IsUpsert = true });
        }

        await _permissionCollection.Indexes.CreateOneAsync(
            new CreateIndexModel<Permission>(
                Builders<Permission>.IndexKeys.Ascending(p => p.Code),
            new CreateIndexOptions { Unique = true }));
    }

    private async Task SeedRoles()
    {


        var roles = new List<Role>
        {
            Role.Administrator,
            Role.Member,
            Role.Ghost
        };

        foreach (var role in roles)
        {
            var filter = Builders<Role>.Filter.Eq(r => r.Name, role.Name);
            await _roleCollection.ReplaceOneAsync(
                filter,
                role,
                new ReplaceOptions { IsUpsert = true });
        }

        await _roleCollection.Indexes.CreateOneAsync(
       new CreateIndexModel<Role>(
           Builders<Role>.IndexKeys.Ascending(r => r.Name),
           new CreateIndexOptions { Unique = true }));
    }
}
