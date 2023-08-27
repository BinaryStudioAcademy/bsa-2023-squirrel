namespace Squirrel.Core.DAL.Entities.JoinEntities;

public sealed class PullRequestReviewer
{
    public int PullRequestId { get; set; }
    public int UserId { get; set; }
    public PullRequest PullRequest { get; set; } = null!;
    public User User { get; set; } = null!;
}