using Microsoft.AspNetCore.SignalR;

namespace Squirrel.Core.BLL.Hubs;

public class CustomUserIdProvider : IUserIdProvider
{
    public virtual string GetUserId(HubConnectionContext connection)
    {
        var claim = connection.User?.FindFirst("ClientId");

        if (claim is null)
        {
            return connection.ConnectionId;
        }

        // If received ClientId from connection is empty -
        // it means that it's first Client's connection
        // and CustomUserIdProvider must generate new uniq guid for it
        return claim.Value.Equals(string.Empty) ? Guid.NewGuid().ToString() : claim.Value;
    }
}