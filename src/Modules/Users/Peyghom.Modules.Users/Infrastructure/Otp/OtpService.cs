namespace Peyghom.Modules.Users.Infrastructure.Otp;

internal sealed class OtpService : IOtpService
{
    public string GenerateCode()
    {
        var random = new Random();

        var generatedRandomNumber = random.Next(100000, 999999);

        return generatedRandomNumber.ToString();
    }
}
