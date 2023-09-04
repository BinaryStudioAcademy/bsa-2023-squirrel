namespace Squirrel.Core.Common.DTO.Auth;

public sealed class UserRegisterDto : UserLoginDto
{
    public string Username { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}