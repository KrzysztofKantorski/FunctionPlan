namespace Application.Exceptions
{
    public class RefreshTokenNotFoundException: AppException
    {
        public RefreshTokenNotFoundException(string message) : base(message, 400) { }
    
    }
}
