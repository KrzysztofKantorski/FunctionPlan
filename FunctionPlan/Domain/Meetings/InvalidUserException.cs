using Domain.Common;

namespace Domain.Meetings
{
    public sealed class InvalidUserException: DomainException
    {
        public InvalidUserException(string message) : base(message, 400) 
        {

        }
    }
}
