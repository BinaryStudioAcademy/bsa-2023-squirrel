using System.Net;
using Squirrel.ExceptionHandling.Enums;
using Squirrel.ExceptionHandling.Exceptions.Abstract;

namespace Squirrel.ExceptionHandling.Exceptions;

public sealed class UsernameAlreadyRegisteredException : RequestException
{
    public UsernameAlreadyRegisteredException() : base(
        "Username is already registered. Try another one",
        ErrorType.InvalidUsername,
        HttpStatusCode.BadRequest)
    {
    }
}