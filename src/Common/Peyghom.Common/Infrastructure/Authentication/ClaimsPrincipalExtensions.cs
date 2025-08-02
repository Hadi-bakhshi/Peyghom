using Peyghom.Common.Application.Exceptions;
using System.Security.Claims;

namespace Peyghom.Common.Infrastructure.Authentication;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirst(CustomClaims.Sub)?.Value;

        return userId ??
            throw new PeyghomException("User identifier is unavailable");
    }

    public static string GetIdentityId(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
               throw new PeyghomException("User identity is unavailable");
    }

    public static string GetRoleName(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirst(ClaimTypes.Role)?.Value
                ?? throw new PeyghomException("User role is unavailable");
    }

    public static HashSet<string> GetPermissions(this ClaimsPrincipal? principal)
    {
        IEnumerable<Claim> permissionClaims = principal?.FindAll(CustomClaims.Permission) ??
                                              throw new PeyghomException("Permissions are unavailable");

        return permissionClaims.Select(c => c.Value).ToHashSet();
    }
}
