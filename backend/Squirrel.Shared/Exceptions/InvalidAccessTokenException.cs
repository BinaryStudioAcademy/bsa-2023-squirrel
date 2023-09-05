using System.Net;
using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions.Abstract;

namespace Squirrel.Shared.Exceptions;

public sealed class InvalidAccessTokenException : RequestException
{
    public InvalidAccessTokenException() : base(
        "Invalid access token!",
        ErrorType.InvalidToken,
        HttpStatusCode.BadRequest)
    {
    }
}