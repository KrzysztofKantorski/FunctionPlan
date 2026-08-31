namespace Application.Common.Dto
{
    public sealed record TokenResponseDto(
        string accessToken,
        string refreshToken
    );
}
