namespace Squirrel.Core.Common.Exceptions;

public sealed class UsernameAlreadyRegisteredException : Exception
{
    public UsernameAlreadyRegisteredException() : base("Username is already registered. Try another one")
    {
    }
}