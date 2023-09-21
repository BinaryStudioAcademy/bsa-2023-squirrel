using Squirrel.SqlService.BLL.Models.ConsoleAppHub;

namespace Squirrel.Core.BLL.Interfaces;

public interface IConsoleConnectService
{
    Task TryConnect(RemoteConnect remoteConnect);
}