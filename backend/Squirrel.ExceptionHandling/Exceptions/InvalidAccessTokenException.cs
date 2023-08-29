namespace Squirrel.ExceptionHandling.Exceptions;

public sealed class InvalidAccessTokenException : Exception
{
    public InvalidAccessTokenException() : base("Invalid access token.")
    {
    }
}