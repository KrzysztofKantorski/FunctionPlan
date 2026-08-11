using Domain.Common;

namespace Domain.Meetings
{
    public sealed class IncorrectLocationException: DomainException
    {
        public IncorrectLocationException(string message): base(message, 400)
        {

        }
    }
}
