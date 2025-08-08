using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Peyghom.Common;
using Peyghom.Modules.Chat.Infrastructure.Repository.Chats;
using Peyghom.Modules.Chat.Infrastructure.Repository.Messages;

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
    {
        services.AddSignalR();

        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
    }

    
}
