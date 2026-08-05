namespace Application.Exceptions
{
    public class InvalidRequestData : AppException
    {
        public InvalidRequestData(string message) : base(message, 400)
        {
        }
    
    }
}
