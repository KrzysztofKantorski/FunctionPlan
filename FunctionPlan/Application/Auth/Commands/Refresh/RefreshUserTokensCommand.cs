using Application.Abstractions.Messaging;

namespace Application.Auth.Commands.Refresh
{
    public sealed record RefreshUserTokensCommand(
        string accessToken,
        string refreshToken

        ) : ICommand<AccessTokenResponseDto>;
}
