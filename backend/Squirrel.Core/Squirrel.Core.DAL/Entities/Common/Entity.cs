namespace Squirrel.Core.DAL.Entities.Common;

public abstract class Entity<T> where T : struct
{
    public T Id { get; set; }
}