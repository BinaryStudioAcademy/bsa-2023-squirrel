using Squirrel.Core.Common.DTO.Auth;

namespace Squirrel.Core.BLL.Interfaces;

public interface IAuthService
{
    Task<RefreshedAccessTokenDto> LoginAsync(UserLoginDto userLoginDto);
    Task<RefreshedAccessTokenDto> RegisterAsync(UserRegisterDto userRegisterDto);
}