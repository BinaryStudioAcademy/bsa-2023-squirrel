namespace Squirrel.Core.DAL.Entities.Common.AuditEntity;

public abstract class AuditEntity<T> : Entity<T> where T : struct
{
    public T? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    public AuditEntity()
    {
        CreatedAt = DateTime.UtcNow;
    }
}