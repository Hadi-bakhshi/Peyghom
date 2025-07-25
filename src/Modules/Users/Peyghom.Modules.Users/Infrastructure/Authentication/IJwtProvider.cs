using System.Security.Claims;

namespace Peyghom.Modules.Users.Infrastructure.Authentication;

public interface IJwtProvider
{
    string GenerateAccessToken();
    string GenerateRefreshToken();
    /// <summary>
    /// This method is called to generate a token
    /// used in otp verification
    /// </summary>
    /// <returns>string</returns>
    string GenerateVerificationToken();
    ClaimsPrincipal? ValidateExpiredToken(string token);
}
