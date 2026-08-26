using Domain.Common;

namespace Domain.Media
{
    public sealed class IncorrectUserException: DomainException
    {
        public IncorrectUserException(string message) : base(message, 400)
        {

        }
    }
}
