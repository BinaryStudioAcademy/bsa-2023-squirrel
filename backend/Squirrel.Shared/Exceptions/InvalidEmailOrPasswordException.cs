using System.Net;
using Squirrel.Shared.Exceptions.Abstract;
using Squirrel.Shared.Enums;

namespace Squirrel.Shared.Exceptions;

public class InvalidEmailOrPasswordException : RequestException
{
    public InvalidEmailOrPasswordException() : base(
        "Invalid email or password.",
        ErrorType.InvalidEmailOrPassword,
        HttpStatusCode.BadRequest)
    {
    }
}