using Domain.Users;

namespace Domain.RefreshTokens
{
    internal class Refreshtoken
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string Token { get; private set; } = string.Empty;
        public DateTime ExpiresAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsRevoked { get; private set; } = false;

        //Relationship with User entity
        public User User { get; private set; }

        private Refreshtoken() { }
        public Refreshtoken(string token, DateTime expiresAt, int userId)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token cannot be null or empty.");
            }

            if (expiresAt <= DateTime.UtcNow)
            {
                throw new ArgumentException("Incorrect token expiry date.");
            }

            if (userId <= 0) 
            { 
                throw new ArgumentException("Incorrect userId value");
            }

            Token = token;
            CreatedAt = expiresAt;
            UserId = userId;
        }


        //Revoke token method
        public void Revoke()
        {
            IsRevoked = true;
        }
    }
}
