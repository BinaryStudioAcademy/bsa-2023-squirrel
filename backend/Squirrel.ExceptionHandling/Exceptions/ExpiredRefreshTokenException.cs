namespace Squirrel.ExceptionHandling.Exceptions;

public sealed class ExpiredRefreshTokenException : Exception
{
    public ExpiredRefreshTokenException() : base("Refresh token expired.")
    {
    }
}