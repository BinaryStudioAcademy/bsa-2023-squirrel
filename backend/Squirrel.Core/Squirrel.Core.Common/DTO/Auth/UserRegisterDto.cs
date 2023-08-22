namespace Squirrel.Core.Common.DTO.Auth;

public sealed class UserRegisterDto : UserLoginDto
{
    public string Username { get; set; }
    public string ConfirmPassword { get; set; }
}