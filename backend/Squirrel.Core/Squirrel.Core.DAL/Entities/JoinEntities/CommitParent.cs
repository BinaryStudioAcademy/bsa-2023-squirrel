using Squirrel.Core.DAL.Entities.Common;

namespace Squirrel.Core.DAL.Entities.JoinEntities;

public sealed class CommitParent : Entity<int>
{
    public int CommitId { get; set; }
    public int ParentId { get; set; }
    public Commit Commit { get; set; } = null!;
    public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    public ICollection<BranchCommit> BranchCommits { get; set; } = new List<BranchCommit>();
}