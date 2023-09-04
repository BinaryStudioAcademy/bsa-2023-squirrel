using AutoMapper;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.Interfaces;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Google.Apis.Auth;
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
        var googleCredentials = await GoogleJsonWebSignature.ValidateAsync(googleCredentialsToken,
            new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string> { _googleClientId }
            });

        var user = await _userService.GetUserByEmailAsync(googleCredentials.Email) ?? await _userService.CreateUserAsync(
                       _mapper.Map<UserRegisterDto>(googleCredentials), isGoogleAuth: true);
        
        return new AuthUserDto
        {
            User = _mapper.Map<UserDto>(user),
            Token = await GenerateNewAccessTokenAsync(user.Id, user.Username, user.Email)
        };
    }

    public async Task<RefreshedAccessTokenDto> LoginAsync(UserLoginDto userLoginDto)
    {
        var userEntity = await _userService.GetUserByEmailAsync(userLoginDto.Email);

        if (userEntity is null ||
            !SecurityUtils.ValidatePassword(userLoginDto.Password, userEntity.PasswordHash!, userEntity.Salt!))
        {
            throw new InvalidEmailOrPasswordException();
        }

        return await GenerateNewAccessTokenAsync(userEntity.Id, userEntity.Username, userLoginDto.Email);
    }

    public async Task<RefreshedAccessTokenDto> RegisterAsync(UserRegisterDto userRegisterDto)
    {
        var createdUser = await _userService.CreateUserAsync(userRegisterDto, isGoogleAuth: false);
        
        return await GenerateNewAccessTokenAsync(createdUser.Id, createdUser.Username, createdUser.Email);
    }

    private async Task<RefreshedAccessTokenDto> GenerateNewAccessTokenAsync(int userId, string userName, string email)
    {
        var refreshToken = _jwtFactory.GenerateRefreshToken();

        _context.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            UserId = userId
        });

        await _context.SaveChangesAsync();

        var accessToken = await _jwtFactory.GenerateAccessTokenAsync(userId, userName, email);

        return new RefreshedAccessTokenDto(accessToken, refreshToken);
    }
}