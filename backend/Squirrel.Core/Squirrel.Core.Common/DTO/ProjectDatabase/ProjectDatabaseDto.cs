namespace Squirrel.Core.Common.DTO.ProjectDatabase;

public sealed class ProjectDatabaseDto
{
    public string DbName { get; set; } = string.Empty;
    public Guid Guid { get; set; }
    public int ProjectId { get; set; }
}