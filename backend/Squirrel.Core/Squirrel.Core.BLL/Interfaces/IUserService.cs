using Microsoft.AspNetCore.Http;
using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.DTO.Users;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(int id);
    Task<User> GetUserByIdInternalAsync(int id);
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<UserDto> GetUserByUsernameAsync(string username);
    Task<UserDto> CreateUserAsync(UserRegisterDto userDto, bool isGoogleAuth);
    Task<User?> GetUserEntityByEmailAsync(string email);
    Task<User?> GetUserEntityByUsernameAsync(string username);
    Task<List<UserDto>> GetAllUsersAsync();
    Task<UserProfileDto> GetUserProfileAsync();
    Task<UserProfileDto> UpdateUserNamesAsync(UpdateUserNamesDto updateUserDto);
    Task ChangePasswordAsync(UpdateUserPasswordDto userDto);
}