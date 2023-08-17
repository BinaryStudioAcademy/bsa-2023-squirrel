using Microsoft.AspNetCore.SignalR;
using Squirrel.Notifier.Hubs.Interfaces;

namespace Squirrel.Notifier.Hubs
{
    public class BroadcastHub : Hub<IBroadcastHubClient>
    {
    }
}