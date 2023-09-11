using System.Net;
using Squirrel.Shared.Exceptions.Abstract;
using Squirrel.Shared.Enums;

namespace Squirrel.Shared.Exceptions;

public class InvalidPasswordException : RequestException
{
    public InvalidPasswordException() : base("Invalid password.",
        ErrorType.InvalidPassword, HttpStatusCode.BadRequest) { }
}