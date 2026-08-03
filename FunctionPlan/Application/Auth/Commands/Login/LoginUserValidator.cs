using FluentValidation;

namespace Application.Auth.Commands.Login
{
    public sealed class LoginUserValidator: AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required")
                .EmailAddress().WithMessage("Invalid email address");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .Length(8, 20).WithMessage("Incorrect password");
        }
    }
}
