namespace Squirrel.Core.Common.DTO.Users;

public class ChangePasswordDTO
{
    public int Id { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}
