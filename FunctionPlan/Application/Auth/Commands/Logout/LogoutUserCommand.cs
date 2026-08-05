using Application.Abstractions.Messaging;

namespace Application.Auth.Commands.Logout
{
    public sealed record LogoutUserCommand(
        string refreshToken
    ) : ICommand;
}
