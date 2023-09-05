namespace Squirrel.Core.Common.DTO.Users;

public class UpdateUserPasswordDto
{
    public int Id { get; set; }
    public string CurrentPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
