using Domain.Common;

namespace Domain.Users
{
    public sealed class InvalidUserDataException: DomainException
    {
        public InvalidUserDataException(string message) : base(message, 400)
        {

        }
    }
}
