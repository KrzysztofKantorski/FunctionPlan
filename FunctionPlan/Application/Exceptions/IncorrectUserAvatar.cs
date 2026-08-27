namespace Application.Exceptions
{
    internal class IncorrectUserAvatar : AppException
    {
        public IncorrectUserAvatar(string message) : base(message, 400)
        {
        }
    }
}
