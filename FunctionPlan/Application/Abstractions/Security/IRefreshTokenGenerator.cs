namespace Application.Abstractions.Security
{
    public interface IRefreshTokenGenerator
    {
        public string GenerateRefreshToken();
    }
}
