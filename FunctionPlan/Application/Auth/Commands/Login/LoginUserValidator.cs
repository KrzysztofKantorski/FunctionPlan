using FluentValidation;

namespace Application.Auth.Commands.Login
{
    public sealed class LoginUserValidator: AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator() 
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email address");
            RuleFor(x => x.Password)
                .Length(8, 20).WithMessage("Incorrect password");
        }
    }
}
