using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions.Abstract;
using System.Net;

namespace Squirrel.Shared.Exceptions;

public sealed class HttpRequestException : RequestException
{
    public HttpRequestException(string message) : base(
        message,
        ErrorType.HttpRequest,
        HttpStatusCode.BadRequest)
    {
    }
}