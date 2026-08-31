using Application.Abstractions.Messaging;

namespace Application.Auth.Commands.LoginWithGoogle
{
    public sealed record LoginWithGoogleCommand(
        string GoogleIdToken
    ):ICommand;
}
