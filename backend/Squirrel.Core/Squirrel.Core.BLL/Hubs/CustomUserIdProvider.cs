using Microsoft.AspNetCore.SignalR;

namespace Squirrel.Core.BLL.Hubs;

public class CustomUserIdProvider : IUserIdProvider
{
    public virtual string GetUserId(HubConnectionContext connection)
    {
        // TODO: find a way to set UserIdentifier with value from ConsoleApp
        // or Delete IUserIdProvider implementation if current way is ok
        return Guid.NewGuid().ToString();
    }
}