using AutoMapper;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.Interfaces;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.Services;

public sealed class AuthService : BaseService, IAuthService
{
    private IJwtFactory _jwtFactory;
    
    public AuthService(SquirrelCoreContext context, IMapper mapper, IJwtFactory jwtFactory) : base(context, mapper)
    {
        _jwtFactory = jwtFactory;
    }

    public async Task<RefreshedAccessTokenDto> LoginAsync(UserLoginDto userLoginDto)
    {
        // TODO: Find user in database by his email and get user info. Exception when not found or invalid credentials.
        // Dummy user info.
        var userId = 0;
        var username = "username";
        
        return await GenerateNewAccessTokenAsync(userId, username, userLoginDto.Email);
    }

    public async Task<RefreshedAccessTokenDto> RegisterAsync(UserRegisterDto userRegisterDto)
    {
        // Dummy user info.
        var userId = 0;
        
        return await GenerateNewAccessTokenAsync(userId, userRegisterDto.Username, userRegisterDto.Email);
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