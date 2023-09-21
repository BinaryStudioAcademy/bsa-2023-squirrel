using Microsoft.Extensions.Configuration;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.SqlService.BLL.Models.ConsoleAppHub;

namespace Squirrel.Core.BLL.Services;

public class ConsoleConnectService : IConsoleConnectService
{
    private readonly IHttpClientService _httpClientService;
    private readonly IConfiguration _configuration;

    public ConsoleConnectService(IHttpClientService httpClientService, IConfiguration configuration)
    {
        _httpClientService = httpClientService;
        _configuration = configuration;
    }

    public async Task TryConnect(RemoteConnect remoteConnect)
    {
        await _httpClientService.SendAsync($"{_configuration["SqlServiceUrl"]}/api/ConsoleAppHub/db-connect",
            remoteConnect, HttpMethod.Post);
    }
}