namespace Squirrel.Core.Common.DTO.Auth;

public class UserLoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = null!;
}