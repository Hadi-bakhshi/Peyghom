using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Peyghom.Common.Presentation.Endpoints;

namespace Peyghom.Modules.Users.Features.VerifyOtp;

internal sealed class VerifyOtpEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(("auth/verify"), (VerifyOtpRequest request) =>
        {
            return Task.FromResult("ok");
        }).RequireAuthorization(Permissions.VerifyUserOtp).WithTags(Tags.Users);
    }
}


internal sealed record VerifyOtpRequest(string Code);