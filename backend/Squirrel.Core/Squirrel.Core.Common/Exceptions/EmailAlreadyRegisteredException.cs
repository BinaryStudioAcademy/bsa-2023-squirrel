using System.Net;
using Squirrel.Core.Common.Enums;
using Squirrel.Core.Common.Exceptions.Abstract;

namespace Squirrel.Core.Common.Exceptions;

public sealed class EmailAlreadyRegisteredException : RequestException
{
    public EmailAlreadyRegisteredException() : base(
        "Email is already registered. Try another one",
        ErrorType.InvalidEmail,
        HttpStatusCode.BadRequest)
    {
    }
}