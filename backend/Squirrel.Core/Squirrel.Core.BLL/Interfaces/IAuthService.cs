using System.Security.Claims;
using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.DTO.Users;

namespace Squirrel.Core.BLL.Interfaces;

public interface IAuthService
{
    Task<RefreshedAccessTokenDto> LoginAsync(UserLoginDto userLoginDto);
    Task<RefreshedAccessTokenDto> RegisterAsync(UserRegisterDto userRegisterDto);
    Task<AuthUserDto> AuthorizeWithGoogleAsync(string googleCredentialsToken);
    int GetUserIdFromToken(IEnumerable<Claim> claims);
}