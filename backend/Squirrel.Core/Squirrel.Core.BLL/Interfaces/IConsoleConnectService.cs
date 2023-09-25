using Squirrel.Shared.DTO.ConsoleAppHub;

namespace Squirrel.Core.BLL.Interfaces;

public interface IConsoleConnectService
{
    Task TryConnect(RemoteConnect remoteConnect);
}