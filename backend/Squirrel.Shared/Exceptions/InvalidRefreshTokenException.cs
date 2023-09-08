using System.Net;
using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions.Abstract;

namespace Squirrel.Shared.Exceptions;

public sealed class InvalidRefreshTokenException : RequestException
{
    public InvalidRefreshTokenException() : base(
        "Invalid refresh token!",
        ErrorType.InvalidToken,
        HttpStatusCode.BadRequest)
    {
    }
}