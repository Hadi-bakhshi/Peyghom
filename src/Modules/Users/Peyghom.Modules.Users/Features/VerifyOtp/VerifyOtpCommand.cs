using Peyghom.Common.Application.Messaging;

namespace Peyghom.Modules.Users.Features.VerifyOtp;

public sealed record VerifyOtpCommand(
    string Code,
    string UserIdentifier,
    string IdentifierType) : ICommand<bool>;
