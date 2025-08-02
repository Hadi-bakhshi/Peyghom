using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Peyghom.Common;
using Peyghom.Common.Application.Authorization;
using Peyghom.Modules.Users.Infrastructure.Authentication;
using Peyghom.Modules.Users.Infrastructure.Authorization;
using Peyghom.Modules.Users.Infrastructure.Otp;
using Peyghom.Modules.Users.Infrastructure.Repository.Contacts;
using Peyghom.Modules.Users.Infrastructure.Repository.UserNotifications;
using Peyghom.Modules.Users.Infrastructure.Repository.Users;
using Peyghom.Modules.Users.Infrastructure.Seeding;

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
        services.AddScoped<IPermissionService, PermissionService>();

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddScoped<IOtpService, OtpService>();

        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IContactRepository, ContactRepository>();

        services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();

        services.AddScoped<DatabaseSeeder>();
    }

    public static async Task SeedUsersDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
        await seeder.SeedAsync();
    }


}
