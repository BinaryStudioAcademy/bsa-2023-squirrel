using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.DTO.Users;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(int id);
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<UserDto> GetUserByUsernameAsync(string username);
    Task<UserDto> CreateUserAsync(UserRegisterDto userDto, bool isGoogleAuth);
    Task<UserDto> UpdateUserNamesAsync(UpdateUserNamesDto updateUserDTO);
    Task ChangePasswordAsync(UpdateUserPasswordDto userDto);
    Task<UserDto> UpdateNotificationsAsync(UpdateUserNotificationsdDto updateNotificationsdDTO);
    Task<User?> GetUserEntityByEmail(string email);
    Task<User?> GetUserEntityByUsername(string username);
}