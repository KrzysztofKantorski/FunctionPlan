using Domain.Common;

namespace Domain.Comments
{
    public sealed class InvalidMeetingException : DomainException
    {
        public InvalidMeetingException(string message) : base(message, 400)
        {
        }
    }
}
