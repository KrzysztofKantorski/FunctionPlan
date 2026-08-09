namespace Application.Exceptions
{
    public class UserAlerdyVerifiedException: AppException
    {
        public UserAlerdyVerifiedException(string message) : base(message, 400)
        {

        }

    }
}
