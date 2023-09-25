namespace Squirrel.Core.Common.DTO.Branch;

public class BranchCreateDto
{
    public string Name { get; set; } = null!;
    public int? ParentId { get; set; }
}
