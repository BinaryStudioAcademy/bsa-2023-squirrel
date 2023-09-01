using Microsoft.AspNetCore.SignalR;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.SquirrelHub;
using Squirrel.Core.DAL.Enums;

namespace Squirrel.Core.BLL.Hubs;

public sealed class SquirrelHub : Hub<ISquirrelHub>
{
    public override async Task OnConnectedAsync()
    {
        var clientId = Guid.NewGuid().ToString();
        await Clients.Caller.SetClientId(clientId);
    }

    // TODO: work with data recived from ConsoleApp (save to Db)
    public async Task ReceiveData(string clientId, string query, DbEngine dbEngine, QueryResultTableDTO queryResultTableDTO)
    {
        Console.WriteLine($"------------------------------------------------------------------");
        Console.WriteLine($"Successfully get data from user '{clientId}'");
        Console.WriteLine($"    query:     '{query}'");
        Console.WriteLine($"    db engine: '{dbEngine}'");
        Console.WriteLine($"    result:");
        Console.WriteLine(queryResultTableDTO);
        Console.WriteLine($"------------------------------------------------------------------");
    }
}