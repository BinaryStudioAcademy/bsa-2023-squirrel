using Squirrel.Core.DAL.Entities.JoinEntities;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.Common.DTO.Branch;

public sealed class BranchDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
    public int ProjectId { get; set; }
    public int? ParentBranchId { get; set; }

    public ICollection<Commit> Commits { get; set; } = new List<Commit>();
    public ICollection<PullRequest> PullRequestsFromThisBranch { get; set; } = new List<PullRequest>();
    public ICollection<PullRequest> PullRequestsIntoThisBranch { get; set; } = new List<PullRequest>();
}