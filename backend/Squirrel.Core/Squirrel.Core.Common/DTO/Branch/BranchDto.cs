namespace Squirrel.Core.Common.DTO.Branch;

public sealed class BranchDto
{
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
    public int ProjectId { get; set; }
}