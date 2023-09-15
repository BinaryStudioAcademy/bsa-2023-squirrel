using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Squirrel.Shared.Exceptions;
public class BranchAlreadyExistException : RequestException
{
    public BranchAlreadyExistException() : base(
        "Branch with this name is alreay exist in project",
        ErrorType.InvalidBranchName,
        HttpStatusCode.BadRequest)
    {
    }
}
