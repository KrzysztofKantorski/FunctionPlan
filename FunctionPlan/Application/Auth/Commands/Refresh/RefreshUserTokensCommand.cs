using Application.Abstractions.Messaging;
using Application.Common.Dto;

namespace Application.Auth.Commands.Refresh
{
    public sealed record RefreshUserTokensCommand(
        string refreshToken

        ) : ICommand<TokenResponseDto>;
}
