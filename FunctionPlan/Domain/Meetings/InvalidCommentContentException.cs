using Domain.Common;

namespace Domain.Meetings
{
    public sealed class InvalidCommentContentException: DomainException
    {
        public InvalidCommentContentException(string message): base(message, 400)
        { 

        }
    }
}
