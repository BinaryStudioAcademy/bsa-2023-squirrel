using System.Net;
using Squirrel.ExceptionHandling.Enums;
using Squirrel.ExceptionHandling.Exceptions.Abstract;

namespace Squirrel.ExceptionHandling.Exceptions;

public sealed class EmailAlreadyRegisteredException : RequestException
{
    public EmailAlreadyRegisteredException() : base(
        "Email is already registered. Try another one",
        ErrorType.InvalidEmail,
        HttpStatusCode.BadRequest)
    {
    }
}