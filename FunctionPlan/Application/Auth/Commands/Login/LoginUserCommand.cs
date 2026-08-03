using Application.Abstractions.Messaging;

namespace Application.Auth.Commands.Login
{
    public sealed record LoginUserCommand(
        string Email,
        string Password
    ): ICommand<TokenResponseDto>;
}
