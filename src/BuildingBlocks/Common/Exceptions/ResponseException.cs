using System;
using System.Net;

namespace Common.Exceptions
{
    public class ResponseException : Exception
    {
        public ResponseException(string message, object errorDetails = null)
        {
            Message = message;
            ErrorDetails = errorDetails;
        }

        public override string Message { get; }
        public object ErrorDetails { get; }
    }
}
