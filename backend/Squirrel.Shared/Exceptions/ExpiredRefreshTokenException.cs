namespace Squirrel.Shared.Exceptions;

public sealed class ExpiredRefreshTokenException : Exception
{
    public ExpiredRefreshTokenException() : base("Refresh token expired.")
    {
    }
}