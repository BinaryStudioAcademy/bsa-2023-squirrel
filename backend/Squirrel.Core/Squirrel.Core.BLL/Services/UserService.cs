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
    
    public UserService(SquirrelCoreContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
        => _mapper.Map<UserDto>( await _context.Users.FirstOrDefaultAsync(u => u.Id == id));

    public async Task<User?> GetUserByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetUserByUsernameAsync(string username)
        => await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

    public async Task<User> CreateUserAsync(UserRegisterDto userDto, bool isGoogleAuth)
    {
        if (await GetUserByUsernameAsync(userDto.Username) is not null)
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

        if (await GetUserByEmailAsync(userDto.Email) is not null)
        {
            throw new EmailAlreadyRegisteredException();
        }

        var newUser = PrepareNewUserData(userDto, isGoogleAuth);
        var createdUser = (await _context.Users.AddAsync(newUser)).Entity;
        await _context.SaveChangesAsync();

        return createdUser;
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