namespace Squirrel.Notifier.WebAPI.Hubs.Interfaces;

public interface IBroadcastHubClient
{
    Task BroadcastMessage(string msg);
}