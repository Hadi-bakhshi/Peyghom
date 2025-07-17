using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Peyghom.Common;

namespace Peyghom.Modules.Chat;

public static class ChatModule
{
    public static IServiceCollection AddChatModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddInfrastructure(configuration);

        services.AddEndpoints(AssemblyReference.Assembly);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {}

    
}
