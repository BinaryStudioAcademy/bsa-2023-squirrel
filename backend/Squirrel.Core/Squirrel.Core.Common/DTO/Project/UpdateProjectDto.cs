namespace Squirrel.Core.Common.DTO.Project;

public sealed class UpdateProjectDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}