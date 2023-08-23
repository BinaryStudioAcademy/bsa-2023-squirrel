namespace Squirrel.Core.Common.Exceptions;

public sealed class IncorrectPasswordConfirmationException : Exception
{
    public IncorrectPasswordConfirmationException() : base("Password/Repeat password don’t match")
    {
    }
}