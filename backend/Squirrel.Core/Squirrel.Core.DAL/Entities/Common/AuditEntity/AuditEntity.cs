namespace Squirrel.Core.DAL.Entities
{
    public abstract class AuditEntity<T> : Entity<T> where T : struct
    {
        public T? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public AuditEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}