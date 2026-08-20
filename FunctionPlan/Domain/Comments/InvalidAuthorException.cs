using Domain.Common;

namespace Domain.Comments
{
    public sealed class InvalidAuthorException : DomainException
    {
        public InvalidAuthorException(string message) : base(message, 400)
        {
        }
    }
}
