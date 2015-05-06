using System;

namespace Finance
{
    public class DomainException : Exception
    {
        public DomainException(string message)
            : base(message)
        {
        }
    }
}
