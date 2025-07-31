using Peyghom.Common.Application.Caching;
using Peyghom.Common.Application.Messaging;
using Peyghom.Common.Domain;
using Peyghom.Modules.Users.Domain;
using Peyghom.Modules.Users.Infrastructure.Authentication;
using Peyghom.Modules.Users.Infrastructure.Otp;

namespace Peyghom.Modules.Users.Features.SendOtp;

internal sealed class SendOtpCommandHandler(
    ICacheService cacheService,
    IOtpService otpService,
    IJwtProvider jwtProvider)
    : ICommandHandler<SendOtpCommand, SendOtpResponse>
{
    public async Task<Result<SendOtpResponse>> Handle(SendOtpCommand request, CancellationToken cancellationToken)
    {

        var foundValue = await cacheService.GetAsync<string>(request.Target, cancellationToken);

        if (foundValue is not null)
        {
            return Result.Failure<SendOtpResponse>(Errors.OtpExist);
        }

        var code = otpService.GenerateCode();

        await cacheService.SetAsync(request.Target, code, TimeSpan.FromMinutes(2), cancellationToken);

        var accessToken = jwtProvider.GenerateVerificationToken(request.Target);

        return Result.Success(new SendOtpResponse(accessToken));
    }
}