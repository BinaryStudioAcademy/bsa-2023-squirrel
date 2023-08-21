using AutoMapper;
using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.DTO.Users;
using Squirrel.Core.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squirrel.Core.BLL.Services
{
    public sealed class AuthService : BaseService
    {
        public AuthService(SquirrelCoreContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<AuthUserDTO> AuthorizeWithGoogle(GoogleTokenDTO userDto)
        {
            //var userEntity = await _context.Users
            //    .Include(u => u.Avatar)
            //    .FirstOrDefaultAsync(u => u.Email == userDto.Email);
            //
            //if (userEntity == null)
            //{
            //    throw new NotFoundException(nameof(User));
            //}
            //
            //var user = _mapper.Map<UserDTO>(userEntity);

            return new AuthUserDTO
            {
                //User = user,
                //Token = token
            };
        }

    }
}
