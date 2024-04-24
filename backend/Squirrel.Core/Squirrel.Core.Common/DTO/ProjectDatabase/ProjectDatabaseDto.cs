namespace Squirrel.Core.Common.DTO.ProjectDatabase;

public sealed class ProjectDatabaseDto
{
    public string DbName { get; set; } = string.Empty;
    public string Guid { get; set; } = null!;
    public int ProjectId { get; set; }
}