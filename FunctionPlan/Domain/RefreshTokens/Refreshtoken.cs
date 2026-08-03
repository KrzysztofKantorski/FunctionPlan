using Domain.Users;

namespace Domain.RefreshTokens
{
    public sealed class RefreshToken
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string Token { get; private set; } = string.Empty;
        public DateTime ExpiresAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsRevoked { get; private set; } = false;

        //Relationship with User entity
        public User User { get; private set; }

        private RefreshToken() { }
        public RefreshToken(string token, DateTime expiresAt, int userId)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidUserDataException("Token cannot be null or empty.");
            }

            if (expiresAt <= DateTime.UtcNow)
            {
                throw new InvalidUserDataException("Incorrect token expiry date.");
            }

            if (userId <= 0) 
            { 
                throw new InvalidUserDataException("Incorrect userId value");
            }

            Token = token;
            ExpiresAt = expiresAt;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }


        //Revoke token method
        public void Revoke()
        {
            IsRevoked = true;
        }
    }
}
