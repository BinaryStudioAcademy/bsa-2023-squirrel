using Squirrel.Core.Common.DTO.Console;

namespace Squirrel.Core.BLL.Interfaces;

public interface IConsoleConnectService
{
    Task TryConnect(RemoteConnect remoteConnect);
}