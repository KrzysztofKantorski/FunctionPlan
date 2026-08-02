using Domain.Common;

namespace Domain.RefreshTokens
{
    public sealed class InvalidUserDataException: DomainException
    {
        public InvalidUserDataException(string message) : base(message, 400)
        {
        }
    }
}
