using System;

namespace SourceMaps
{
    public class InvalidSourceMapException : Exception
    {
        public InvalidSourceMapException()
        {
        }

        public InvalidSourceMapException(string message)
            : base(message)
        {
        }

        public InvalidSourceMapException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
