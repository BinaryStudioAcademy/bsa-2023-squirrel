using System.Net;
using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions.Abstract;

namespace Squirrel.Shared.Exceptions;

public class BranchAlreadyExistException : RequestException
{
    public BranchAlreadyExistException() : base(
        "Branch with this name already exists in project",
        ErrorType.InvalidBranchName,
        HttpStatusCode.BadRequest)
    {
    }
}
