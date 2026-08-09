using FluentValidation;

namespace Application.Auth.Commands.VerifyUserEmail
{
    public sealed class VerifyUserEmailCommandValidator: AbstractValidator<VerifyUserEmailCommand>
    {
        public VerifyUserEmailCommandValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required")
                .EmailAddress().WithMessage("Incorrect email address");

            RuleFor(x => x.OTP)
                .NotEmpty().WithMessage("Verification code is required")
                .Length(6).WithMessage("Verification code must be exactly 6 characters")
                .Matches("^[0-9]+$").WithMessage("Verification code must contain only digits");
        }
    }
}
