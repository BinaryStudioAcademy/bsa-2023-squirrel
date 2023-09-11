using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.AzureBlobStorage.Models;
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
    private readonly IUserIdGetter _userIdGetter;
    private readonly IBlobStorageService _blobStorageService;

    public UserService(SquirrelCoreContext context, IMapper mapper, IUserIdGetter userIdGetter,
        IBlobStorageService blobStorageService) : base(context, mapper)
    {
        _userIdGetter = userIdGetter;
        _blobStorageService = blobStorageService;
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        return _mapper.Map<UserDto>(await GetUserByIdInternal(id));
    }

    public async Task<UserProfileDto> GetUserProfileAsync()
    {
        return await _context.Users.ProjectTo<UserProfileDto>(_mapper.ConfigurationProvider)
            .FirstAsync(x => x.Id == _userIdGetter.GetCurrentUserId());
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
        var userEntity = await GetUserEntityByUsername(username);
        if (userEntity == null)
        {
            throw new EntityNotFoundException(nameof(User), username);
        }

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

        return _mapper.Map<UserDto>(createdUser);
    }

    public async Task<UserProfileDto> UpdateUserNamesAsync(UpdateUserNamesDto updateUserDto)
    {
        var userEntity = await GetUserByIdInternal(_userIdGetter.GetCurrentUserId());

        var existingUserWithSameUsername = await GetUserEntityByUsername(updateUserDto.Username);

        if (existingUserWithSameUsername != null && existingUserWithSameUsername.Id != _userIdGetter.GetCurrentUserId())
        {
            throw new UsernameAlreadyRegisteredException();
        }

        _mapper.Map(updateUserDto, userEntity);

        _context.Users.Update(userEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserProfileDto>(userEntity);
    }

    public async Task ChangePasswordAsync(UpdateUserPasswordDto changePasswordDto)
    {
        var userEntity = await GetUserByIdInternal(_userIdGetter.GetCurrentUserId());

        if (!SecurityUtils.ValidatePassword(changePasswordDto.CurrentPassword, userEntity.PasswordHash!,
                userEntity.Salt!))
        {
            throw new InvalidPasswordException();
        }

        userEntity.PasswordHash = SecurityUtils.HashPassword(changePasswordDto.NewPassword, userEntity.Salt!);

        _context.Users.Update(userEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<UserProfileDto> UpdateNotificationsAsync(UpdateUserNotificationsDto updateNotificationsDto)
    {
        var userEntity = await GetUserByIdInternal(_userIdGetter.GetCurrentUserId());

        userEntity.SquirrelNotification = updateNotificationsDto.SquirrelNotification;
        userEntity.EmailNotification = updateNotificationsDto.EmailNotification;

        _context.Users.Update(userEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserProfileDto>(userEntity);
    }

    public async Task<User?> GetUserEntityByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserEntityByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
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

    public async Task<string> AddAvatar(IFormFile avatar)
    {
        using var ms = new MemoryStream();
        await avatar.CopyToAsync(ms);

        var blob = new Blob
        {
            Id = _userIdGetter.GetCurrentUserId().ToString(),
            ContentType = avatar.ContentType,
            Content = ms.ToArray()
        };

        var url = await _blobStorageService.UploadWithUrlAsync("user-avatars", blob);

        return url;
    }
}