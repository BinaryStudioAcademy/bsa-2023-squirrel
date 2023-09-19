﻿using Microsoft.Extensions.Configuration;
using Squirrel.Core.DAL.Entities;
using Squirrel.Shared.Interfaces;

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

    public async Task SaveDBStructureToAzureBlob(ChangeRecord changeRecord, Guid clientId)
    {
        await _httpClientService.PostAsync<Guid>
            ($"{_configuration["SqlServiceUrl"]}/api/Changes/{clientId}", changeRecord.UniqueChangeId);
    }
}