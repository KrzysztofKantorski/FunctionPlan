namespace Application.Exceptions
{
    public class UserNotVerifiedException: AppException
    {
        public UserNotVerifiedException(string message): base(message, 403)
        {

        }
    }
}
