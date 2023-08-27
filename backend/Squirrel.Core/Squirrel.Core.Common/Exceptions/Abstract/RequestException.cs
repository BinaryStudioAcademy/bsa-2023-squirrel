using System.Net;
using Squirrel.Core.Common.Enums;

namespace Squirrel.Core.Common.Exceptions.Abstract;

public abstract class RequestException : Exception
{
    public ErrorType ErrorType { get; }
    public HttpStatusCode StatusCode { get; }

    public RequestException(string message, ErrorType errorType, HttpStatusCode statusCode) : base(message)
    {
        ErrorType = errorType;
        StatusCode = statusCode;
    }
}