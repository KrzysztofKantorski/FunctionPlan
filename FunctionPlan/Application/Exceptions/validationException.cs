namespace Application.Exceptions
{
    public class ValidationException: ApplicationException
    {
        public IEnumerable<ValidationError> Errors { get; }
        public ValidationException(IEnumerable<ValidationError> errors) 
        {
            Errors = errors;
        }
    }
}
