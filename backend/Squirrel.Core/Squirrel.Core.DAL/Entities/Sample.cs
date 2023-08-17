namespace Squirrel.Core.DAL.Entities
{
    public class Sample : AuditEntity<long>
    {
        public string Title { get; set; } = string.Empty;
        public string? Body { get; set; }
    }
}
