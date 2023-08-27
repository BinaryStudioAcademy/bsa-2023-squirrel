using System.ComponentModel.DataAnnotations;

namespace Squirrel.Core.DAL.Entities.JoinEntities;

public sealed class BranchCommit
{
    [Required]
    public bool IsMerged { get; set; }
    [Required]
    public bool IsHead { get; set; }
    
    public int BranchId { get; set; }
    public int CommitParentId { get; set; }
    public Branch Branch { get; set; } = null!;
    public CommitParent CommitParent { get; set; } = null!;
}