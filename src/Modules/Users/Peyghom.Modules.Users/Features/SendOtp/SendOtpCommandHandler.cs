using Peyghom.Common.Application.Caching;
using Peyghom.Common.Application.Messaging;
using Peyghom.Common.Domain;
using Peyghom.Modules.Users.Domain;

namespace Peyghom.Modules.Users.Features.SendOtp;

internal sealed class SendOtpCommandHandler(ICacheService cacheService) : ICommandHandler<SendOtpCommand, SendOtpResponse>
{
    public async Task<Result<SendOtpResponse>> Handle(SendOtpCommand request, CancellationToken cancellationToken)
    {
        // Generate OTP code

        // check if there is no code exist for the phone number 
        var foundValue = await cacheService.GetAsync<string>(request.Target, cancellationToken);
        if (foundValue is not null)
        {
            return Result.Failure<SendOtpResponse>(Errors.OtpExist);
        }
        // Save it to the redis with two minutes expiration time
        // use the phone number as key and the value would be the generated OTP
        await cacheService.SetAsync(request.Target, "12345", TimeSpan.FromMinutes(2), cancellationToken);

        // generate an access token with two claims 

        // one claim is the phone number and the other one is the route that can user can call

        // return the access token to the client

        return Result.Success(new SendOtpResponse("545sd4fsd54sd5"));
    }
}