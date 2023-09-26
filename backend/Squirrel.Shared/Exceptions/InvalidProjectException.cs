using System.Net;
using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions.Abstract;

namespace Squirrel.Shared.Exceptions;

public sealed class InvalidProjectException : RequestException
{
    public InvalidProjectException() : base(
        "Such project does not exist!",
        ErrorType.InvalidProject,
        HttpStatusCode.BadRequest)
    {
    }
}