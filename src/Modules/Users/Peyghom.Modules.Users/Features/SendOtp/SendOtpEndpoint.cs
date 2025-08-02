using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Peyghom.Common.Domain;
using Peyghom.Common.Presentation.Endpoints;
using Peyghom.Common.Presentation.Results;

namespace Peyghom.Modules.Users.Features.SendOtp;

internal sealed class SendOtpEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/otp", async (SendOtpRequest request, ISender sender) =>
            {
                Result<SendOtpResponse> result = await sender.Send(new SendOtpCommand(request.Target, request.Type));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .AllowAnonymous()
            //.RequireAuthorization(Permissions.VerifyUserOtp)
            .WithTags(Tags.Users);
    }
}

internal sealed record SendOtpRequest(string Target, string Type);