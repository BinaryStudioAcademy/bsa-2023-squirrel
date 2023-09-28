using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions.Abstract;
using System.Net;

namespace Squirrel.Shared.Exceptions;

public sealed class UserCannotBeRemovedException : RequestException
{
    public UserCannotBeRemovedException() : base(
        "User can not be removed.",
        ErrorType.UserCannotBeRemoved,
        HttpStatusCode.BadRequest)
    {
    }
}