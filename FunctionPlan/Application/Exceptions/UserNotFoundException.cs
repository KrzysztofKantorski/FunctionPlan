namespace Application.Exceptions
{
    public class UserNotFoundException : AppException
    {
        public UserNotFoundException(string message) : base(message, 404)
        {
        }
    }
}
