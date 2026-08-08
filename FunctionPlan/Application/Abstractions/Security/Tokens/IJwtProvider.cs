using Domain.Users;

namespace Application.Abstractions.Security.Tokens
{
    public interface IJwtProvider
    {
        public string GenerateToken(User user);
    }
}
