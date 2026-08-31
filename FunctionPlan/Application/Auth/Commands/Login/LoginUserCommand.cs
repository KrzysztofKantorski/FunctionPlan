using Application.Abstractions.Messaging;
using Application.Common.Dto;

namespace Application.Auth.Commands.Login
{
    public sealed record LoginUserCommand(
        string Email,
        string Password
    ): ICommand<TokenResponseDto>;
}
