namespace Domain.Common
{
    public abstract class DomainException: Exception
    {
        public int StatusCode { get; }
        protected DomainException(string message, int statusCode) 
        {
            StatusCode = statusCode;
        }
    }
}
