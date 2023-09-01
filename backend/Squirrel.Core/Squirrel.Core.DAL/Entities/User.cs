using System.Collections;
using Squirrel.Core.DAL.Entities.Common;
using Squirrel.Core.DAL.Entities.JoinEntities;

namespace Squirrel.Core.DAL.Entities;

public sealed class User : Entity<int>
{
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PasswordHash { get; set; }
    public string? Salt { get; set; }
    public string? AvatarUrl { get; set; }
    public bool IsGoogleAuth { get; set; }

    public ICollection<Commit> Commits { get; set; } = new List<Commit>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<PullRequest> PullRequests { get; set; } = new List<PullRequest>();
    public ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<PullRequest> ReviewedRequests { get; set; } = new List<PullRequest>();
    public ICollection<PullRequestReviewer> PullRequestReviewers { get; set; } = new List<PullRequestReviewer>();
}