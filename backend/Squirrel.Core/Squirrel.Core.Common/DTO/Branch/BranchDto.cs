namespace Squirrel.Core.Common.DTO.Branch;

public class BranchDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
    public int ProjectId { get; set; }
}