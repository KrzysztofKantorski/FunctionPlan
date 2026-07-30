using Domain.Common;

namespace Domain.Meetings
{
    public sealed class InvalidMeetingStatus: DomainException
    {
        public InvalidMeetingStatus(string message) : base(message, 400) 
        { 
        
        }
    }
}
