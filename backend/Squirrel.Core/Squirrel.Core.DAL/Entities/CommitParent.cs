using Squirrel.Core.DAL.Entities.Common;

namespace Squirrel.Core.DAL.Entities;

public sealed class CommitParent : Entity<int>
{
    public int CommitId { get; set; }
    public int ParentId { get; set; }
    public Commit Commit { get; set; } = null!;
    public Commit ParentCommit { get; set; } = null!;
}