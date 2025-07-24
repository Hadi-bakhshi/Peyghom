using Peyghom.Common.Application.Messaging;

namespace Peyghom.Modules.Users.Features.SendOtp;

public sealed record SendOtpCommand(string PhoneNumber) : ICommand<SendOtpResponse>;