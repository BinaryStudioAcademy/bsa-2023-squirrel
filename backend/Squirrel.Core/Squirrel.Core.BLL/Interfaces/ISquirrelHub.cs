using Squirrel.Core.DAL.Enums;

namespace Squirrel.Core.BLL.Interfaces;

public interface ISquirrelHub
{
    Task SetClientId(string guid);
    Task ExecuteQuery(string clientId, DbEngine dbEngine, string query);
}

