using Domain.Common;

namespace Domain.Media
{
    public sealed class IncorrectFileName : DomainException
    {
        public IncorrectFileName(string message): base(message, 400) 
        { 
        
        }
    }
}
