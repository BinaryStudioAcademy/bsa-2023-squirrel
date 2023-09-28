using Squirrel.Core.DAL.Entities.Common.AuditEntity;
using Squirrel.Core.DAL.Entities.JoinEntities;

namespace Squirrel.Core.DAL.Entities;

public sealed class Commit : AuditEntity<int>
{
    public string Message { get; set; } = string.Empty;
    public string PreScript { get; set; } = null!;
    public string PostScript { get; set; } = null!;
    public bool IsSaved { get; set; }

    public User Author { get; set; } = null!;
    public ICollection<Branch> Branches = new List<Branch>();
    public ICollection<BranchCommit> BranchCommits = new List<BranchCommit>();
    public ICollection<CommitFile> CommitFiles = new List<CommitFile>();
    public ICollection<CommitParent> CommitsAsParent = new List<CommitParent>();
    public ICollection<CommitParent> CommitsAsChild = new List<CommitParent>();
}