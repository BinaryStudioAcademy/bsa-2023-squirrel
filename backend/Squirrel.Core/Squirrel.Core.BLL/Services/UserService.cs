using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Squirrel.Core.BLL.Extensions;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.DTO.Users;
using Squirrel.Core.Common.Security;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Squirrel.Shared.Exceptions;

namespace Squirrel.Core.BLL.Services;

public sealed class UserService : BaseService, IUserService
{
    private const int MaxNameLength = 25;
    private const int MinNameLength = 2;

    public UserService(SquirrelCoreContext context, IMapper mapper) : base(context, mapper) { }
    
    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var userEntity = await GetUserByIdInternal(id);
        return _mapper.Map<UserDto>(userEntity);
    }

    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        var userEntity = await GetUserEntityByEmail(email);
        if (userEntity == null)
        {
            throw new EntityNotFoundException(nameof(User), email);
        }
        return _mapper.Map<UserDto>(userEntity);
    }

    public async Task<UserDto> GetUserByUsernameAsync(string username)
    {
        var userEntity = await GetUserByUsernameInternal(username);
        return _mapper.Map<UserDto>(userEntity);
    }

    public async Task<UserDto> CreateUserAsync(UserRegisterDto userDto, bool isGoogleAuth)
    {
        if (await GetUserEntityByUsername(userDto.Username) is not null)
        {
            if (isGoogleAuth)
            {
                // Google registration must not fail because of the same username.
                userDto.Username = GenerateRandomUsername();
            }
            else
            {
                throw new UsernameAlreadyRegisteredException();
            }
        }

        if (await GetUserEntityByEmail(userDto.Email) is not null)
        {
            throw new EmailAlreadyRegisteredException();
        }

        var newUser = PrepareNewUserData(userDto, isGoogleAuth);
        var createdUser = (await _context.Users.AddAsync(newUser)).Entity;
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(createdUser); ;
    }

    public async Task<UserDto> UpdateUserNamesAsync(UpdateUserNamesDto updateUserDTO)
    {
        var userEntity = await GetUserByIdInternal(updateUserDTO.Id);

        if (await GetUserByUsernameInternal(updateUserDTO.Username) is not null)
        {
            throw new UsernameAlreadyRegisteredException();
        }

        _mapper.Map(updateUserDTO, userEntity);

        _context.Users.Update(userEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(userEntity);
    }

    public async Task ChangePasswordAsync(UpdateUserPasswordDto changePasswordDTO)
    {
        var userEntity = await GetUserByIdInternal(changePasswordDTO.Id);

        if (!SecurityUtils.ValidatePassword(changePasswordDTO.CurrentPassword, userEntity.PasswordHash!, userEntity.Salt!))
        {
            throw new InvalidEmailOrPasswordException();
        }

        userEntity.PasswordHash = SecurityUtils.HashPassword(changePasswordDTO.NewPassword, userEntity.Salt!);

        _context.Users.Update(userEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<UserDto> UpdateNotificationsAsync(UpdateUserNotificationsdDto updateNotificationsdDTO)
    {
        var userEntity = await GetUserByIdInternal(updateNotificationsdDTO.Id);

        userEntity.SquirrelNotification = updateNotificationsdDTO.SquirrelNotification;
        userEntity.EmailNotification = updateNotificationsdDTO.EmailNotification;

        _context.Users.Update(userEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(userEntity);
    }

    public async Task<User?> GetUserEntityByEmail(string email)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return userEntity;
    }

    public async Task<User?> GetUserEntityByUsername(string username)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        return userEntity;
    }

    private async Task<User> GetUserByIdInternal(int id)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (userEntity == null)
        {
            throw new EntityNotFoundException(nameof(User), id);
        }

        return userEntity;
    }

    private async Task<User?> GetUserByUsernameInternal(string username)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        return userEntity;
    }

    private string GenerateRandomUsername()
        => ("user" + Guid.NewGuid()).Truncate(MaxNameLength);

    private void AdaptUserNames(UserRegisterDto user)
    {
        user.FirstName = user.FirstName.PadRight(MinNameLength, '-').Truncate(MaxNameLength);
        user.LastName = user.LastName.PadRight(MinNameLength, '-').Truncate(MaxNameLength);
    }

    private void HashUserPassword(User newUser, string password)
    {
        var salt = SecurityUtils.GenerateRandomSalt();
        newUser.Salt = salt;
        newUser.PasswordHash = SecurityUtils.HashPassword(password, salt);
    }

    private User PrepareNewUserData(UserRegisterDto userDto, bool isGoogleAuth)
    {
        // User's first name and last name from Google account might be too long or too short,
        // so we need to adapt it to meet our requirements.
        AdaptUserNames(userDto);
        var newUser = _mapper.Map<User>(userDto)!;
        newUser.IsGoogleAuth = isGoogleAuth;
        if (!isGoogleAuth)
        {
            HashUserPassword(newUser, userDto.Password);
        }

        return newUser;
    }
}