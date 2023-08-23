using Microsoft.AspNetCore.SignalR;
using Squirrel.Notifier.WebAPI.Hubs.Interfaces;

namespace Squirrel.Notifier.WebAPI.Hubs;

public class BroadcastHub : Hub<IBroadcastHubClient>
{
}