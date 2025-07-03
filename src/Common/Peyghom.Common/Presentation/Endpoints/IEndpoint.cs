using Microsoft.AspNetCore.Routing;

namespace Peyghom.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
