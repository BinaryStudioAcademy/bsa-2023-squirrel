using System.Net;
using Squirrel.Core.Common.Enums;
using Squirrel.Core.Common.Exceptions.Abstract;

namespace Squirrel.Core.Common.Exceptions;

public class NotFoundException : RequestException
{
    public NotFoundException(string name, int id)
        : base($"Entity {name} with id ({id}) was not found.", ErrorType.NotFound,
            HttpStatusCode.NotFound) { }

    public NotFoundException(string name) : base($"Entity {name} was not found.", ErrorType.NotFound,
        HttpStatusCode.NotFound) { }
}