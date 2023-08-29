using System.Net;
using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions.Abstract;

namespace Squirrel.Shared.Exceptions;

public sealed class EmailAlreadyRegisteredException : RequestException
{
    public EmailAlreadyRegisteredException() : base(
        "Email is already registered. Try another one",
        ErrorType.InvalidEmail,
        HttpStatusCode.BadRequest)
    {
    }
}