namespace Squirrel.Core.Common.Exceptions;

public class InvalidUsernameOrPasswordException: Exception
{
    public InvalidUsernameOrPasswordException() : base("Invalid username or password.") { }
}