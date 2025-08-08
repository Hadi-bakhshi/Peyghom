using FluentValidation;

namespace Peyghom.Modules.Chat.Features.SendMessage;

public sealed class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(p => p.SenderId)
            .NotEmpty()
            .WithMessage("SenderId cannot be empty")
            .NotNull()
            .WithMessage("SenderId is required");
        
        RuleFor(p => p.ChatId)
            .NotEmpty()
            .WithMessage("ChatId cannot be empty")
            .NotNull()
            .WithMessage("ChatId is required");
        
        RuleFor(p => p.Content)
            .NotEmpty()
            .WithMessage("Content cannot be empty")
            .NotNull()
            .WithMessage("Content is required");

    }
}