using Squirrel.Core.DAL.Entities.Common.AuditEntity;
using Squirrel.Core.DAL.Enums;

namespace Squirrel.Core.DAL.Entities;

public sealed class Comment : AuditEntity<int>
{
    public string Content { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
    public CommentedEntity CommentedEntity { get; set; }
    
    public int CommentedEntityId { get; set; }

    public Comment()
    {
        UpdatedAt = CreatedAt;
    }
}