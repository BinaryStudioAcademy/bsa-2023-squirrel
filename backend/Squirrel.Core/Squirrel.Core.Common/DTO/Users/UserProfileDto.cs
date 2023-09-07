namespace Squirrel.Core.Common.DTO.Users;

public sealed class UserProfileDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? AvatarUrl { get; set; }
    public bool SquirrelNotification { get; set; }
    public bool EmailNotification { get; set; }
    public bool IsGoogleAuth { get; set; }
}
