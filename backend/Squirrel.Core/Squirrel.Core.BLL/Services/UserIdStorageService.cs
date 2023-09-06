using Squirrel.Core.BLL.Interfaces;
using Squirrel.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Squirrel.Core.BLL.Services;
public class UserIdStorageService : IUserIdGetter, IUserIdSetter
{
    private int _userId;

    public int GetCurrentUserId()
    {
        if (_userId == 0)
        {
            throw new InvalidAccessTokenException();
        }

        return _userId;
    }

    public void SetCurrentUserId(int id)
    {
        _userId = id;
    }
}
