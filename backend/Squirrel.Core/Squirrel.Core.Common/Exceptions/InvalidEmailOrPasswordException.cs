using System.Net;
using Squirrel.Core.Common.Enums;
using Squirrel.Core.Common.Exceptions.Abstract;

namespace Squirrel.Core.Common.Exceptions;

public class InvalidEmailOrPasswordException : RequestException
{
    public InvalidEmailOrPasswordException() : base("Invalid email or password.",
        ErrorType.InvalidEmailOrPassword, HttpStatusCode.BadRequest) { }
}