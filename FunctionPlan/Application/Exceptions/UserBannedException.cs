namespace Application.Exceptions
{
    public class UserBannedException: AppException
    {
        public UserBannedException(string message): base(message, 403)
        {

        }
    }
}
