namespace Application.Auth.Commands.Login
{
   public sealed record TokenResponseDto
   (
       string AccessToken,
       string RefreshToken
   );
}
