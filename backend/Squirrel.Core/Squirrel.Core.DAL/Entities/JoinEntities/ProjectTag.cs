namespace Squirrel.Core.DAL.Entities.JoinEntities;

public sealed class ProjectTag
{
    public int TagId { get; set; }
    public int ProjectId { get; set; }
    public Tag Tag { get; set; } = null!;
    public Project Project { get; set; } = null!;
}