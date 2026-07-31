using Domain.Common;

namespace Domain.Users
{
    public sealed class InvalidUserCredentialsException: DomainException
    {
        public InvalidUserCredentialsException(string message): base(message, 400)
        {

        }
    }
}
