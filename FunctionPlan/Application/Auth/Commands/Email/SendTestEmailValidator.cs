using FluentValidation;

namespace Application.Auth.Commands.Email
{
    public sealed class SendTestEmailValidator : AbstractValidator<SendTestEmailCommand>
    {
        public SendTestEmailValidator()
        {
            RuleFor(x => x.To)
                .EmailAddress().WithMessage("Invalid email address");
            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Subject cannot be empty");
            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Body cannot be empty");
        }
    }
}
