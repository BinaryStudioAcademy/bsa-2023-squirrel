using Microsoft.Extensions.Configuration;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.Interfaces;

public class DbStructureSaverService : IDbStructureSaverService
{
    private readonly IHttpClientService _httpClientService;
    private readonly IConfiguration _configuration;

    public DbStructureSaverService(IHttpClientService httpClientService, IConfiguration configuration)
    {
        _httpClientService = httpClientService;
        _configuration = configuration;
    }

    public async Task SaveDbStructureToAzureBlobAsync(ChangeRecord changeRecord, Guid clientId)
    {
        await _httpClientService.SendAsync
            ($"{_configuration["SqlServiceUrl"]}/api/Changes/{clientId}", changeRecord.UniqueChangeId, HttpMethod.Post);
    }
}