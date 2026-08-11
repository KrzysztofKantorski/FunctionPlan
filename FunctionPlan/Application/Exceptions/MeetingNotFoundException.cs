namespace Application.Exceptions
{
    public sealed class MeetingNotFoundException: AppException
    {
        public MeetingNotFoundException(string message): base(message, 404)
        {

        }
    }
}
