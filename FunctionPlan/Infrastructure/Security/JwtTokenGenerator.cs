using Application.Abstractions.Security;
using Domain.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Security
{
    internal sealed class JwtTokenGenerator
        (JwtSettings settings): IJwtProvider
    {
        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                //User id
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),

                //Email
                new Claim(JwtRegisteredClaimNames.Email, user.Email),

                //Token unique id
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 

                //User role
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };


            var token = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(settings.ExpiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
