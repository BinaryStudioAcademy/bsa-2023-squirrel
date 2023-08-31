namespace Squirrel.Core.Common.DTO.Users;

public class UpdateUserPasswordDTO
{
    public int Id { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}
