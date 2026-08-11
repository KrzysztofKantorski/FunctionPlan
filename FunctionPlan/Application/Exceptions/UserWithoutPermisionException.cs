namespace Application.Exceptions
{
    public sealed class UserWithoutPermisionException: AppException
    {
        public UserWithoutPermisionException(string message): base(message, 403)
        {

        }
    }
}
