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
    /// <param name="identifier"></param>
    /// <param name="identifierType">this parameter values can be phone or email</param>
    /// <returns></returns>
    string GenerateVerificationToken(string identifier, string identifierType);
    ClaimsPrincipal? ValidateExpiredToken(string token);
}
