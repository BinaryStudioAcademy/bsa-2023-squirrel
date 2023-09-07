using Squirrel.Core.Common.DTO.Branch;

namespace Squirrel.Core.Common.DTO.Project;

public sealed class NewProjectDto
{
    public ProjectDto Project { get; set; } = null!;
    public BranchDto DefaultBranch { get; set; } = null!;
}