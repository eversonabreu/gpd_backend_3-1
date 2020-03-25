using System;

namespace GPD.Backend.Domain.Exceptions
{
    [Serializable]
    public class ExpirationTokenException : Exception
    {
        public ExpirationTokenException(string message, Exception innerException) : base(message, innerException) { }

        public ExpirationTokenException(string message) : base(message) { }
    }
}
