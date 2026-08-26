using Domain.Common;

namespace Domain.Media
{
    public sealed class IncorrectImageDescription: DomainException
    { 
        public IncorrectImageDescription(string message): base(message, 400)
        {

        }
    }
}
