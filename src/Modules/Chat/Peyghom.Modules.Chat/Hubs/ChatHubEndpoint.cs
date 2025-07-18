using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Peyghom.Common.Presentation.Endpoints;

namespace Peyghom.Modules.Chat.Hubs;

internal sealed class ChatHubEndpoint : IHubEndpoint
{
    public void MapHub(IEndpointRouteBuilder app)
    {
        app.MapHub<ChatHub>("/chatHub");
    }
}