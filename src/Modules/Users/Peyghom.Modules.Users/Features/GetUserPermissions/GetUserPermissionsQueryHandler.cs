using MongoDB.Driver;
using Peyghom.Common.Application.Authorization;
using Peyghom.Common.Application.Messaging;
using Peyghom.Common.Domain;
using Peyghom.Modules.Users.Domain;
using System.Security;

namespace Peyghom.Modules.Users.Features.GetUserPermissions;

internal sealed class GetUserPermissionsQueryHandler : IQueryHandler<GetUserPermissionsQuery, PermissionsResponse>
{

    private readonly IMongoCollection<Role> _roleCollection;
    private readonly IMongoCollection<Permission> _permissionCollection;
    private readonly IMongoCollection<User> _userCollection;
    public GetUserPermissionsQueryHandler(IMongoClient mongoClient)
    {
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase("peyghom");
        _roleCollection = mongoDatabase.GetCollection<Role>("role");
        _permissionCollection = mongoDatabase.GetCollection<Permission>("permission");
        _userCollection = mongoDatabase.GetCollection<User>("user");
    }

    public async Task<Result<PermissionsResponse>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
    {

        if (request.IsGhost)
        {
            return new PermissionsResponse(request.UserId, Role.Ghost.Permissions.ToHashSet());
        }

        var userCursor = await _userCollection
     .FindAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);
        var user = await userCursor.FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure<PermissionsResponse>(Errors.NotFound(request.UserId));
        }

        if (user.RoleNames == null || !user.RoleNames.Any())
        {
            return Result.Failure<PermissionsResponse>(Errors.NoRoleAssigned);
        }

        // Load roles by name
        var roleFilter = Builders<Role>.Filter.In(r => r.Name, user.RoleNames);
        var roleCursor = await _roleCollection.FindAsync(roleFilter, cancellationToken: cancellationToken);
        var roles = await roleCursor.ToListAsync(cancellationToken);

        if (!roles.Any())
        {
            return Result.Failure<PermissionsResponse>(Errors.NoMatchingRole);
        }

        // Aggregate permission codes from roles
        var permissionCodes = roles
            .SelectMany(r => r.Permissions)
            .Where(code => !string.IsNullOrWhiteSpace(code))
            .Distinct()
            .ToHashSet();

        return new PermissionsResponse(user.Id, permissionCodes);

    }

}
