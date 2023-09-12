using System.Net;
using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions.Abstract;

namespace Squirrel.Shared.Exceptions;

public sealed class ExpiredRefreshTokenException : RequestException
{
    public ExpiredRefreshTokenException() : base(
        "Refresh token expired.",
        ErrorType.RefreshTokenExpired,
        HttpStatusCode.Forbidden)
    {
    }
}