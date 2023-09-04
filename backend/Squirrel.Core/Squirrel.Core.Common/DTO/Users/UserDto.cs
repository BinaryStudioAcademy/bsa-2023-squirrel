namespace Squirrel.Core.Common.DTO.Users;

public sealed class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = String.Empty;
    public string UserName { get; set; } = String.Empty;
    public string? AvatarUrl { get; set; }
}
