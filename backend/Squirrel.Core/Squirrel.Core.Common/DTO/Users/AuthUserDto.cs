using Squirrel.Core.Common.DTO.Auth;

namespace Squirrel.Core.Common.DTO.Users;

public sealed class AuthUserDto
{
    public UserDto User { get; set; }
    public RefreshedAccessTokenDto Token { get; set; }
}
