using FluentValidation;

namespace Application.Auth.Commands
{
    public sealed class RegisterUserCommandValidator: AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required")
                .EmailAddress().WithMessage("Invalid email address");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .Length(3, 30).WithMessage("Invalid username");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .Length(8,20).WithMessage("Password must be between 8 and 20 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
        }
    }
}
