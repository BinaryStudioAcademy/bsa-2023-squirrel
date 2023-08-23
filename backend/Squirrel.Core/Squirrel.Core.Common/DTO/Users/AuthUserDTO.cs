namespace Squirrel.Core.Common.DTO.Users;

public sealed class AuthUserDTO
{
    public UserDTO User { get; set; }
    public AccessTokenDTO Token { get; set; }
}
