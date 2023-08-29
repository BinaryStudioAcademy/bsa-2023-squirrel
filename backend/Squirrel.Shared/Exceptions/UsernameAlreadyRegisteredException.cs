using System.Net;
using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions.Abstract;

namespace Squirrel.Shared.Exceptions;

public sealed class UsernameAlreadyRegisteredException : RequestException
{
    public UsernameAlreadyRegisteredException() : base(
        "Username is already registered. Try another one",
        ErrorType.InvalidUsername,
        HttpStatusCode.BadRequest)
    {
    }
}