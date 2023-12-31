﻿using Microsoft.Extensions.Configuration;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Shared.DTO.DatabaseItem;

namespace Squirrel.Core.BLL.Services;

public class DatabaseItemsService : IDatabaseItemsService
{
    private const string DatabaseItemsRoutePrefix = "/api/DatabaseItems/";
    private readonly IHttpClientService _httpClientService;
    private readonly IConfiguration _configuration;

    public DatabaseItemsService(IHttpClientService httpClientService, IConfiguration configuration)
    {
        _httpClientService = httpClientService;
        _configuration = configuration;
    }

    public async Task<ICollection<DatabaseItem>> GetAllItemsAsync(Guid clientId)
    {
        return await _httpClientService.GetAsync<List<DatabaseItem>>
            ($"{_configuration[BaseService.SqlServiceUrlSection]}{DatabaseItemsRoutePrefix}{clientId}");
    }
}
