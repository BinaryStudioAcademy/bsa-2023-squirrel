using Squirrel.Core.Common.JWT;

namespace Squirrel.Core.Common.DTO.Auth;

public sealed class RefreshedAccessTokenDto
{
    public AccessToken AccessToken { get; }
    public string RefreshToken { get; }

    public RefreshedAccessTokenDto(AccessToken accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}