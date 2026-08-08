namespace Application.Abstractions.Security.Tokens
{
    public interface IRefreshTokenGenerator
    {
        public string GenerateRefreshToken();
    }
}
