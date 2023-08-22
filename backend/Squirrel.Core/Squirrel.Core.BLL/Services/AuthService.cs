using AutoMapper;
using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.DTO.Users;
using Squirrel.Core.DAL.Context;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Squirrel.Core.BLL.Interfaces;

namespace Squirrel.Core.BLL.Services
{
    public sealed class AuthService : BaseService, IAuthService
    {
        private readonly string _googleClientId;

        public AuthService(SquirrelCoreContext context, IMapper mapper, IOptions<AuthenticationSettings> authSettings) : base(context, mapper)
        {
            _googleClientId = authSettings.Value.GoogleClientId;
        }

        public async Task<AuthUserDTO> AuthorizeWithGoogle(GoogleToken googleToken)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(googleToken.IdToken, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string> { _googleClientId }
            });

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

    }
}
