using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Peyghom.Common;
using Peyghom.Modules.Users.Infrastructure.Authentication;
using Peyghom.Modules.Users.Infrastructure.Otp;

namespace Peyghom.Modules.Users;

public static class UsersModule
{
    public static IServiceCollection AddUsersModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddInfrastructure(configuration);

        services.AddEndpoints(AssemblyReference.Assembly);
    
        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddScoped<IOtpService, OtpService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
    }

    
}
