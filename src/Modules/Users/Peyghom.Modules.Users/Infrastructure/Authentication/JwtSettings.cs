namespace Peyghom.Modules.Users.Infrastructure.Authentication;

public class JwtSettings
{
    public const string SectionName = "Authentication";
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SecretKey {  get; set; } = string.Empty;
    public TimeSpan AccessTokenLifetime { get; set; } = TimeSpan.FromMinutes(15);
    public TimeSpan RefreshTokenLifetime { get; set; } = TimeSpan.FromDays(7);
    public TimeSpan VerificationTokenLifetime { get; set; } = TimeSpan.FromMinutes(2);
}
