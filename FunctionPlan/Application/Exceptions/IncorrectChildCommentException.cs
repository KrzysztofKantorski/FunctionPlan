namespace Application.Exceptions
{
    internal sealed class IncorrectChildCommentException: AppException
    {
        public IncorrectChildCommentException(string message) : base(message, 400)
        {
        }
    }
}
