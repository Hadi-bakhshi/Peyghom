using FluentValidation;

namespace Peyghom.Modules.Users.Features.SendOtp;

internal sealed class SendOtpCommandValidator : AbstractValidator<SendOtpCommand>
{
    public SendOtpCommandValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty()
            .Must(type => type == "email" || type == "phone")
            .WithMessage("Type must be either 'email' or 'phone'.");

        RuleFor(x => x.Target)
            .NotEmpty()
            .WithMessage("Target is required.")
            .DependentRules(() =>
            {
                When(x => x.Type == "email", () =>
                {
                    RuleFor(x => x.Target)
                        .EmailAddress()
                        .WithMessage("Invalid email format.");
                });

                When(x => x.Type == "phone", () =>
                {
                    RuleFor(x => x.Target)
                        .Matches(@"^\+?\d{10,15}$")
                        .WithMessage("Invalid phone number format.");
                });
            });
    }
}
