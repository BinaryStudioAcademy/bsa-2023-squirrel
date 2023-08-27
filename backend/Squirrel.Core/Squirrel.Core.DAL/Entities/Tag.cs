using Squirrel.Core.DAL.Entities.Common;
using Squirrel.Core.DAL.Entities.JoinEntities;

namespace Squirrel.Core.DAL.Entities;

public sealed class Tag : Entity<int>
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<ProjectTag> ProjectTags { get; set; } = new List<ProjectTag>();
}