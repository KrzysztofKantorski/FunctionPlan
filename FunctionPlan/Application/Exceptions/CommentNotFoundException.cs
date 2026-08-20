namespace Application.Exceptions
{
    internal sealed class CommentNotFoundException : AppException
    {
        public CommentNotFoundException(string message) : base(message, 404)
        {
        }
    }
}
