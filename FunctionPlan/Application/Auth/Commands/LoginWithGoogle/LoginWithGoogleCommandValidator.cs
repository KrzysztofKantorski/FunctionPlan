using FluentValidation;


namespace Application.Auth.Commands.LoginWithGoogle
{
    public sealed class LoginWithGoogleCommandValidator :AbstractValidator<LoginWithGoogleCommand>
    {
        public LoginWithGoogleCommandValidator() {

            RuleFor(x => x.GoogleIdToken)
                    .NotEmpty()
                    .MinimumLength(100)

                    //JWT must have 3 parts
                    .Matches(@"^[A-Za-z0-9-_]+\.[A-Za-z0-9-_]+\.[A-Za-z0-9-_]+$")
                    .WithMessage("Incorrect token format.");

        }
    }
}
