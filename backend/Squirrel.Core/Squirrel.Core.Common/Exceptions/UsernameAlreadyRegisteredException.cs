using System.Net;
using Squirrel.Core.Common.Enums;
using Squirrel.Core.Common.Exceptions.Abstract;

namespace Squirrel.Core.Common.Exceptions;

public sealed class UsernameAlreadyRegisteredException : RequestException
{
    public UsernameAlreadyRegisteredException() : base(
        "Username is already registered. Try another one",
        ErrorType.InvalidUsername,
        HttpStatusCode.BadRequest)
    {
    }
}