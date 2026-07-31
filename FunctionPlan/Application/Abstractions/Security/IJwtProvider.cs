using Domain.Users;

namespace Application.Abstractions.Security
{
    public interface IJwtProvider
    {
        public string GenerateToken(User user);
    }
}
