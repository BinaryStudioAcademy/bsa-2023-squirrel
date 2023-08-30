namespace Squirrel.Core.Common.Exceptions;

public sealed class EntityNotFoundException : Exception
{
    public EntityNotFoundException() : base("Entity not found.")
    {
    }
}