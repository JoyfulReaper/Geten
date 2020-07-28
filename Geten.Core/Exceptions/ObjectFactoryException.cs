using System;

namespace Geten.Core.Exceptions
{
    public class ObjectFactoryException : Exception
    {
        public ObjectFactoryException() : base()
        {
        }

        public ObjectFactoryException(string message) : base(message)
        {
        }

        public ObjectFactoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}