using System.Net;
using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions.Abstract;

namespace Squirrel.Shared.Exceptions;

public sealed class NotFoundException : RequestException
{
    public NotFoundException(string entityName) : base(
        $"Entity '{entityName}' not found",
        ErrorType.NotFound,
        HttpStatusCode.NotFound)
    {
    }

    public NotFoundException(string entityName, int id) : base(
        $"Entity '{entityName}' with id '{id}' not found",
        ErrorType.NotFound,
        HttpStatusCode.NotFound)
    {
    }
}