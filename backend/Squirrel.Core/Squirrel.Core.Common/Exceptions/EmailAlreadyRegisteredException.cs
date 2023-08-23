namespace Squirrel.Core.Common.Exceptions;

public sealed class EmailAlreadyRegisteredException : Exception
{
    public EmailAlreadyRegisteredException() : base("Email is already registered. Try another one")
    {
    }
}