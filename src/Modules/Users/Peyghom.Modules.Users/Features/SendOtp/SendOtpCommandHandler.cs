using Peyghom.Common.Application.Messaging;
using Peyghom.Common.Domain;

namespace Peyghom.Modules.Users.Features.SendOtp;

internal sealed class SendOtpCommandHandler : ICommandHandler<SendOtpCommand, SendOtpResponse>
{
    public Task<Result<SendOtpResponse>> Handle(SendOtpCommand request, CancellationToken cancellationToken)
    {
        return Task.Run(() => Result.Success(new SendOtpResponse("545sd4fsd54sd5")), cancellationToken);
    }
}