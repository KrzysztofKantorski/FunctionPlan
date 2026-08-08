using Application.Auth.Commands.UserVerification;
using FluentValidation;

namespace Application.Auth.Commands.SendVerificationEmail
{
    public sealed class SendVerificationEmailCommandValidator: AbstractValidator<SendVerificationEmailCommand>
    {
        public SendVerificationEmailCommandValidator() {
            RuleFor(x => x.Email)
               .EmailAddress().WithMessage("Invalid email address");
        }
    }
}
