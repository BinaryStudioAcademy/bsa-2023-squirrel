using AutoMapper;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.Exceptions;
using Squirrel.Core.Common.Interfaces;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Squirrel.Core.Common.DTO.Users;
using Squirrel.Core.Common.Security;

namespace Squirrel.Core.BLL.Services;

public sealed class AuthService : BaseService, IAuthService
{
    private IJwtFactory _jwtFactory;
    private readonly string _googleClientId;

    public AuthService(
        SquirrelCoreContext context,
        IMapper mapper,
        IJwtFactory jwtFactory,
        IOptions<AuthenticationSettings> authSettings) : base(context, mapper)
    {
        _jwtFactory = jwtFactory;
        _googleClientId = authSettings.Value.GoogleClientId;
    }

    public async Task<AuthUserDTO> AuthorizeWithGoogleAsync(string googleToken)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(googleToken,
            new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string> { _googleClientId }
            });

        // TODO: it will be implemented later, after Users
        //var userEntity = await _context.Users
        //    .Include(u => u.Avatar)
        //    .FirstOrDefaultAsync(u => u.Email == payload.Email);
        //
        //if (userEntity == null)
        //{
        //    throw new NotFoundException(nameof(User));
        //}
        //
        //var user = _mapper.Map<UserDTO>(userEntity);

        //var token = JwtGenerator.GenerateNewToken(user);

        return new AuthUserDTO
        {
            //User = userDTO,
            //Token = acessTokenDTO
        };
    }

    public async Task<RefreshedAccessTokenDto> LoginAsync(UserLoginDto userLoginDto)
    {
        var userEntity = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == userLoginDto.Email);

        if (userEntity == null ||
            !SecurityUtils.ValidatePassword(userLoginDto.Password, userEntity.Password, userEntity.Salt))
        {
            throw new InvalidEmailOrPasswordException();
        }

        return await GenerateNewAccessTokenAsync(userEntity.Id, userEntity.Username, userLoginDto.Email);
    }

    public async Task<RefreshedAccessTokenDto> RegisterAsync(UserRegisterDto userRegisterDto)
    {
        if (await _context.Users.FirstOrDefaultAsync(u => u.Username == userRegisterDto.Username) is not null)
        {
            throw new UsernameAlreadyRegisteredException();
        }

        if (await _context.Users.FirstOrDefaultAsync(u => u.Email == userRegisterDto.Email) is not null)
        {
            throw new EmailAlreadyRegisteredException();
        }

        var newUser = _mapper.Map<User>(userRegisterDto)!;
        var salt = SecurityUtils.GenerateRandomSalt();
        newUser.Salt = salt;
        newUser.PasswordHash = SecurityUtils.HashPassword(newUser.PasswordHash, salt);
        var createdUser = (await _context.Users.AddAsync(newUser)).Entity;
        await _context.SaveChangesAsync();

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