namespace Squirrel.Core.Common.Exceptions;

public sealed class InvalidAccessTokenException : Exception
{
    public InvalidAccessTokenException() : base("Invalid access token.")
    {
    }
}