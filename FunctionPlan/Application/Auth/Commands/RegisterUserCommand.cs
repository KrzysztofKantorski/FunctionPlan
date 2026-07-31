using Application.Abstractions.Messaging;

namespace Application.Auth.Commands
{
    public sealed record RegisterUserCommand(
        string Username,
        string Email,
        string Password
    ): ICommand<int>;
}
