namespace Squirrel.Core.Common.DTO.Auth;

public sealed class UserRegisterDto : UserLoginDto
{
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}