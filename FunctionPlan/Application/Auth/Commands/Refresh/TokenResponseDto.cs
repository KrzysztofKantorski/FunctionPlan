namespace Application.Auth.Commands.Refresh
{
    public sealed record TokenResponseDto(
        string accessToken,
        string refreshToken
    );
}
