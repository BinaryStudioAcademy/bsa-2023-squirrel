using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions.Abstract;
using System.Net;

namespace Squirrel.Shared.Exceptions;

public sealed class InvalidPermissionsException : RequestException
{
    public InvalidPermissionsException() : base(
        "Don't have permission to perform this action",
        ErrorType.InvalidPermission,
        HttpStatusCode.Forbidden)
    {
    }
}