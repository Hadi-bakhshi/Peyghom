using Microsoft.AspNetCore.Routing;

namespace Peyghom.Common.Presentation.Endpoints;

public interface IHubEndpoint
{
    void MapHub(IEndpointRouteBuilder app);
}