using Squirrel.Core.DAL.Entities.Common.AuditEntity;

namespace Squirrel.Core.DAL.Entities;

public sealed class Script : AuditEntity<int>
{
    public string Title { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string Content { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }

    public int ProjectId { get; set; }
    public int LastUpdatedByUserId { get; set; }
    public Project Project { get; set; } = null!;
    public User LastUpdatedBy { get; set; } = null!;
    public User Author { get; set; } = null!;
}