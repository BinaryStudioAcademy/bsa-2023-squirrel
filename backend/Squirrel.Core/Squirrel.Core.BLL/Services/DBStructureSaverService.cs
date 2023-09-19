using Microsoft.Extensions.Configuration;
using Squirrel.AzureBlobStorage.Models;
using Squirrel.Core.DAL.Entities;

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

    public async Task SaveDBStructureToAzureBlob(ChangeRecord changeRecord)
    {
        await _httpClientService.SendAsync<Guid, Blob>
            ($"{_configuration["SqlServiceUrl"]}/api/Changes", changeRecord.UniqueChangeId, HttpMethod.Post);
    }
}