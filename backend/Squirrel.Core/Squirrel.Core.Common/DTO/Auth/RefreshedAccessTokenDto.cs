namespace Squirrel.Core.Common.DTO.Auth;

public sealed class RefreshedAccessTokenDto
{
    public string AccessToken { get; }
    public string RefreshToken { get; }

    public RefreshedAccessTokenDto(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}