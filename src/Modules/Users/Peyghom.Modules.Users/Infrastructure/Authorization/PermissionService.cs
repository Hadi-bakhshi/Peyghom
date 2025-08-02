using MediatR;
using Peyghom.Common.Application.Authorization;
using Peyghom.Common.Domain;
using Peyghom.Modules.Users.Features.GetUserPermissions;

namespace Peyghom.Modules.Users.Infrastructure.Authorization;

internal sealed class PermissionService(ISender sender) : IPermissionService
{
    public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId, bool isGhost)
    {
        return await sender.Send(new GetUserPermissionsQuery(identityId, isGhost));
    }
}
