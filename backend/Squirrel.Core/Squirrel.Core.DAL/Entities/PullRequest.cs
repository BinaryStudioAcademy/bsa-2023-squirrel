using Squirrel.Core.DAL.Entities.Common.AuditEntity;
using Squirrel.Core.DAL.Enums;

namespace Squirrel.Core.DAL.Entities;

public sealed class PullRequest : AuditEntity<int>
{
    public string Title { get; set; } = string.Empty;
    public PullRequestStatus Status { get; set; }
    public bool IsReviewed { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public int ProjectId { get; set; }
    public int SourceBranchId { get; set; }
    public int TargetBranchId { get; set; }
    
    public PullRequest()
    {
        UpdatedAt = CreatedAt;
    }
}
