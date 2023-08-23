using Squirrel.Core.Common.DTO.Auth;

namespace Squirrel.Core.Common.DTO.Users;

public sealed class AccessTokenDTO
{
    public AccessToken AccessToken { get; }
    public string RefreshToken { get; }

    public AccessTokenDTO(AccessToken accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}
