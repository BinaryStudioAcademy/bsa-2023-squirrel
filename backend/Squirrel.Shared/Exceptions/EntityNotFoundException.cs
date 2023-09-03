using System.Net;
using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions.Abstract;

namespace Squirrel.Shared.Exceptions;

public sealed class EntityNotFoundException : RequestException
{
    public EntityNotFoundException(string name) : base($"Entity {name} was not found.",
        ErrorType.NotFound, HttpStatusCode.NotFound) { }
}