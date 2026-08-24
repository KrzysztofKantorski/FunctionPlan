using Domain.Common;
namespace Domain.Users
{
    public sealed class InvalidUserImageException: DomainException
    {
        public InvalidUserImageException(string message): base(message, 400)
        {

        }
    }
}
