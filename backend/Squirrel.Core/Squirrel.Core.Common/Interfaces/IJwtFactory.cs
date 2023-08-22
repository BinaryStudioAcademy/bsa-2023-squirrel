using Squirrel.Core.Common.JWT;

namespace Squirrel.Core.Common.Interfaces;

public interface IJwtFactory
{
    Task<string> GenerateAccessTokenAsync(int id, string userName, string email);
    string GenerateRefreshToken();
    int GetUserIdFromToken(string accessToken, string signingKey);
}