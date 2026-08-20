using Domain.Common;

namespace Domain.Comments
{
    public sealed class EmptyCommentContent : DomainException
    {
        public EmptyCommentContent(string message) : base(message, 400)
        {
        }
    }
}
