using Squirrel.Core.Common.DTO.Auth;

namespace Squirrel.Core.BLL.Interfaces;

public interface IAuthService
{
    Task<RefreshedAccessTokenDto> Login(UserLoginDto userLoginDto);
    Task<RefreshedAccessTokenDto> Register(UserRegisterDto userRegisterDto);
}