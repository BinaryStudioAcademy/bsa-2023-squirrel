using Microsoft.Extensions.Configuration;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Shared.DTO.DatabaseItem;

namespace Squirrel.Core.BLL.Services;

public class DatabaseItemsService : IDatabaseItemsService
{
    private readonly IHttpClientService _httpClientService;
    private readonly IConfiguration _configuration;

    public DatabaseItemsService(IHttpClientService httpClientService, IConfiguration configuration)
    {
        _httpClientService = httpClientService;
        _configuration = configuration;
    }

    public async Task<List<DatabaseItem>> GetAllItemsAsync(Guid clientId)
    {
        return await _httpClientService.GetAsync<List<DatabaseItem>>
            ($"{_configuration["SqlServiceUrl"]}/api/DatabaseItems/{clientId}");
    }
}
