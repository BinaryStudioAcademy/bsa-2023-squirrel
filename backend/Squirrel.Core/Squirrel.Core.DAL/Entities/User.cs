using Squirrel.Core.DAL.Entities.Common;

namespace Squirrel.Core.DAL.Entities;

public sealed class User : Entity<int>
{
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
}