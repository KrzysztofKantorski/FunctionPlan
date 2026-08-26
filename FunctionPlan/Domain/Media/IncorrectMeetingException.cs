using Domain.Common;

namespace Domain.Media
{
    public sealed class IncorrectMeetingException :DomainException
    {
        public IncorrectMeetingException(string message) : base(message, 400) 
        { 

        }
    }
}
