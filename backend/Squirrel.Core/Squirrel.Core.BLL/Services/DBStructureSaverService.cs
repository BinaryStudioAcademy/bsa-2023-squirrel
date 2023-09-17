using Microsoft.Extensions.Configuration;
using Squirrel.Core.DAL.Entities;
using Squirrel.Shared.DTO.DatabaseItem;

namespace Squirrel.Core.BLL.Interfaces;

public class DBStructureSaverService : IDBStructureSaverService
{
    private readonly IHttpClientService _httpClientService;
    private readonly IConfiguration _configuration;

    public DBStructureSaverService(IHttpClientService httpClientService, IConfiguration configuration)
    {
        _httpClientService = httpClientService;
        _configuration = configuration;
    }

    public async Task<ICollection<DatabaseItem>> SaveDBStructureToAzureBlob(ChangeRecord changeRecord, Guid clientId)
    {
        return await _httpClientService.PostAsync<Guid, ICollection<DatabaseItem>>
            ($"{_configuration["SqlServiceUrl"]}/api/Changes/{clientId}", changeRecord.UniqueChangeId);
    }
}