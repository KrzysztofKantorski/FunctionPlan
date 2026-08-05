namespace Application.Auth.Commands.Refresh
{
    public sealed record AccessTokenResponseDto(
        string accessToken,
        string refreshToken
    );
}
