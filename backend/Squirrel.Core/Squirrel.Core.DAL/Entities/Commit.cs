using Squirrel.Core.DAL.Entities.Common.AuditEntity;
using Squirrel.Core.DAL.Entities.JoinEntities;

namespace Squirrel.Core.DAL.Entities;

public sealed class Commit : AuditEntity<int>
{
    public string Message { get; set; } = string.Empty;

    public User Author { get; set; } = null!;
    public ICollection<CommitFile> CommitFiles = new List<CommitFile>();
    public ICollection<CommitParent> CommitParents = new List<CommitParent>();
}