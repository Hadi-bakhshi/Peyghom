using Peyghom.Common.Application.Authorization;
using Peyghom.Common.Application.Messaging;

namespace Peyghom.Modules.Users.Features.GetUserPermissions;

public sealed record GetUserPermissionsQuery(string UserId) : IQuery<PermissionsResponse>;