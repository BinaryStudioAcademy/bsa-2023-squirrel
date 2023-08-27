using Squirrel.Core.DAL.Entities.Common;
using Squirrel.Core.DAL.Entities.JoinEntities;
using Squirrel.Core.DAL.Enums;

namespace Squirrel.Core.DAL.Entities;

public sealed class Project : Entity<int>
{
    public string Name { get; set; } = string.Empty;
    public DbEngine DbEngine { get; set; }

    public int DefaultBranchId { get; set; }
    public ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
    public ICollection<User> Users { get; set; } = new List<User>();
}