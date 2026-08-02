using Application.Abstractions.Security;
using System.Security.Cryptography;

namespace Infrastructure.Security
{
    internal class RefreshTokenGenerator: IRefreshTokenGenerator
    {
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
