namespace Application.Exceptions
{
    internal sealed class CancelledMeetingException : AppException
    {
        public CancelledMeetingException(string message) : base(message, 400)
        {
        }
    }
}
