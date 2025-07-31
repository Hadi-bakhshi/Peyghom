namespace Peyghom.Modules.Users.Infrastructure.Otp;

internal interface IOtpService
{
    /// <summary>
    /// This method will generate 6 digit random number
    /// </summary>
    /// <returns></returns>
    public string GenerateCode();
}
