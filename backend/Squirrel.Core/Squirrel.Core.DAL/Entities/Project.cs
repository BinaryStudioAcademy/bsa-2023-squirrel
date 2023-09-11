using Squirrel.Core.DAL.Entities.Common.AuditEntity;
using Squirrel.Core.DAL.Entities.JoinEntities;
using Squirrel.Core.DAL.Enums;

namespace Squirrel.Core.DAL.Entities;

public sealed class Project : AuditEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DbEngine DbEngine { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public int? DefaultBranchId { get; set; }
    public Branch? DefaultBranch { get; set; } = null!;
    public User Author { get; set; } = null!;
    public ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    public ICollection<PullRequest> PullRequests { get; set; } = new List<PullRequest>();
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public ICollection<ProjectTag> ProjectTags { get; set; } = new List<ProjectTag>();
    public ICollection<Script> Scripts { get; set; } = new List<Script>();
}