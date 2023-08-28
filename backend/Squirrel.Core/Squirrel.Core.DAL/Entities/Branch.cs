using Squirrel.Core.DAL.Entities.Common;
using Squirrel.Core.DAL.Entities.JoinEntities;

namespace Squirrel.Core.DAL.Entities;

public sealed class Branch : Entity<int>
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public ICollection<Commit> Commits { get; set; } = new List<Commit>();
    public ICollection<BranchCommit> BranchCommits { get; set; } = new List<BranchCommit>();
    public ICollection<PullRequest> PullRequestsFromThisBranch { get; set; } = new List<PullRequest>();
    public ICollection<PullRequest> PullRequestsIntoThisBranch { get; set; } = new List<PullRequest>();
}