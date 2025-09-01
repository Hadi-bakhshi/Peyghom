using FluentValidation;

namespace Peyghom.Modules.Users.Features.VerifyOtp;

public sealed class VerifyOtpCommandValidator : AbstractValidator<VerifyOtpCommand>
{
    public VerifyOtpCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Code should not be empty")
            .NotNull()
            .WithMessage("Code is reuired")
            .Must((x) => x.Length == 6)
            .WithMessage("Incorrect length of code")
            .Matches(@"^\d+$")
            .WithMessage("Invalid code content");

    }
}
