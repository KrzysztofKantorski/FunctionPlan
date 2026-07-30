using Domain.Common;

namespace Domain.Meetings
{
    public sealed class InvalidMeetingDateException: DomainException
    {
        public InvalidMeetingDateException(string message) : base(message, 400) 
        {
            
        }
    }
}
