using Squirrel.Core.Common.DTO.Auth;

namespace Squirrel.Core.Common.DTO.Users;

public sealed class AuthUserDTO
{
    public UserDTO User { get; set; }
    public RefreshedAccessTokenDto Token { get; set; }
}
