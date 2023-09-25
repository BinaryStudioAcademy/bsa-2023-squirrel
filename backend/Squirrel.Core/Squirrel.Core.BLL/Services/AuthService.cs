using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.Interfaces;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using static Google.Apis.Auth.GoogleJsonWebSignature;
using Microsoft.Extensions.Options;
using Squirrel.Core.Common.DTO.Users;
using Squirrel.Core.Common.Security;
using Squirrel.Shared.Exceptions;

namespace Squirrel.Core.BLL.Services;

public sealed class AuthService : BaseService, IAuthService
{
    private readonly string _googleClientId;
    private readonly IJwtFactory _jwtFactory;
    private readonly IUserService _userService;

    public AuthService(
        SquirrelCoreContext context,
        IMapper mapper,
        IJwtFactory jwtFactory,
        IOptions<AuthenticationSettings> authSettings,
        IUserService userService) : base(context, mapper)
    {
        _jwtFactory = jwtFactory;
        _userService = userService;
        _googleClientId = authSettings.Value!.GoogleClientId;
    }

    public async Task<AuthUserDto> AuthorizeWithGoogleAsync(string googleCredentialsToken)
    {
        var googleCredentials = await ValidateAsync(googleCredentialsToken, new ValidationSettings { Audience = new List<string> { _googleClientId } });

        UserDto user;
        try
        {
            user = await _userService.GetUserByEmailAsync(googleCredentials.Email);
            await RemoveExpiredRefreshTokensAsync(user.Id);
        }
        catch
        {
            user = await _userService.CreateUserAsync(
                _mapper.Map<UserRegisterDto>(googleCredentials), isGoogleAuth: true);
        }

        return new AuthUserDto
        {
            User = _mapper.Map<UserDto>(user),
            Token = await GenerateNewAccessTokenAsync(user.Id, user.UserName, user.Email)
        };
    }

    public async Task<RefreshedAccessTokenDto> RefreshTokensAsync(RefreshedAccessTokenDto tokens)
    {
        var userId = _jwtFactory.GetUserIdFromToken(tokens.AccessToken);
        var user = await _userService.GetUserByIdAsync(userId);
        
        return new RefreshedAccessTokenDto
        (
            refreshToken: await ExchangeRefreshToken(tokens.RefreshToken, userId),
            accessToken: await _jwtFactory.GenerateAccessTokenAsync(user.Id, user.UserName, user.Email)
        );
    }

    public async Task<AuthUserDto> LoginAsync(UserLoginDto userLoginDto)
    {
        var userEntity = await _userService.GetUserEntityByEmailAsync(userLoginDto.Email);

        if (userEntity is null ||
            !SecurityUtils.ValidatePassword(userLoginDto.Password, userEntity.PasswordHash!, userEntity.Salt!))
        {
            throw new InvalidEmailOrPasswordException();
        }
        
        await RemoveExpiredRefreshTokensAsync(userEntity.Id);
        
        return new AuthUserDto
        {
            User = _mapper.Map<UserDto>(userEntity),
            Token = await GenerateNewAccessTokenAsync(userEntity.Id, userEntity.Username, userEntity.Email)
        };
    }

    public async Task<AuthUserDto> RegisterAsync(UserRegisterDto userRegisterDto)
    {
        var createdUser = await _userService.CreateUserAsync(userRegisterDto, isGoogleAuth: false);

        return new AuthUserDto
        {
            User = _mapper.Map<UserDto>(createdUser),
            Token = await GenerateNewAccessTokenAsync(createdUser.Id, createdUser.UserName, createdUser.Email)
        };
    }

    private async Task<string> ExchangeRefreshToken(string oldRefreshToken, int userId)
    {
        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(
            t => t.Token == oldRefreshToken && t.UserId == userId);

        if (refreshToken is null)
        {
            throw new InvalidRefreshTokenException();
        }

        if (!refreshToken.IsActive())
        {
            throw new ExpiredRefreshTokenException();
        }

        _context.RefreshTokens.Remove(refreshToken);
        var newRefreshToken = _jwtFactory.GenerateRefreshToken();
        _context.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            UserId = userId
        });
        await _context.SaveChangesAsync();

        return newRefreshToken;
    }

    private async Task RemoveExpiredRefreshTokensAsync(int userId)
    {
        var userTokens = await _context.RefreshTokens.Where(x => x.UserId == userId).ToListAsync();
        _context.RefreshTokens.RemoveRange(userTokens.Where(x => !x.IsActive()));
        await _context.SaveChangesAsync();
    }

    private async Task<RefreshedAccessTokenDto> GenerateNewAccessTokenAsync(int userId, string userName, string email)
    {
        var refreshToken = _jwtFactory.GenerateRefreshToken();

        await _context.RefreshTokens.AddAsync(new RefreshToken
        {
            Token = refreshToken,
            UserId = userId
        });
        await _context.SaveChangesAsync();

        var accessToken = await _jwtFactory.GenerateAccessTokenAsync(userId, userName, email);

        return new RefreshedAccessTokenDto(accessToken, refreshToken);
    }
}