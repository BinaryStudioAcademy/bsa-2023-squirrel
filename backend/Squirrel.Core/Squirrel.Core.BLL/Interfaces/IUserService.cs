using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.DAL.Entities;
using Squirrel.Core.Common.DTO.Users;

namespace Squirrel.Core.BLL.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User> CreateUserAsync(UserRegisterDto userDto, bool isGoogleAuth);
    Task<UserDto> GetUserByIdAsync(int id);
    Task<UserDto> UpdateUserAsync(UpdateUserNamesDTO updateUserDTO);
    Task ChangePasswordAsync(UpdateUserPasswordDTO userDto);
    Task<UserDto> UpdateNotificationsAsync(UpdateUserNotificationsdDTO updateNotificationsdDTO);
}