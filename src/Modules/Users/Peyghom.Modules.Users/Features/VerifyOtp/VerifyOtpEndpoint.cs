using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Peyghom.Common.Infrastructure.Authentication;
using Peyghom.Common.Presentation.Endpoints;
using Peyghom.Common.Presentation.Results;
using System.Security.Claims;

namespace Peyghom.Modules.Users.Features.VerifyOtp;

internal sealed class VerifyOtpEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(("auth/verify"), async (
            [FromBody] VerifyOtpRequest request,
            ClaimsPrincipal claims,
            ISender sender) =>
        {
            var result = await sender.Send(new VerifyOtpCommand(
                request.Code,
                claims.GetIdentityId(),
                claims.GetIdentifierType()));

            return result.Match(Results.Ok, ApiResults.Problem);

        }).RequireAuthorization(Permissions.VerifyUserOtp).WithTags(Tags.Users);
    }
}


internal sealed record VerifyOtpRequest(string Code);