using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Peyghom.Modules.Users.Infrastructure.Authentication;

internal sealed class JwtProvider : IJwtProvider
{
    private readonly JwtSettings _jwtSettings;
    public JwtProvider(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }
    public string GenerateAccessToken()
    {
        throw new NotImplementedException();
    }

    public string GenerateVerificationToken()
    {
        throw new NotImplementedException();
    }

    public string GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal? ValidateExpiredToken(string token)
    {
        throw new NotImplementedException();
    }
}
