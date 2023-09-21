using Microsoft.Extensions.Configuration;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.SqlService.BLL.Models.ConsoleAppHub;
using Squirrel.SqlService.BLL.Models.DTO;

namespace Squirrel.Core.BLL.Services;

public class TableService : ITableService
{
    private readonly IHttpClientService _httpClientService;
    private readonly IConfiguration _configuration;

    public TableService(IHttpClientService httpClientService, IConfiguration configuration)
    {
        _httpClientService = httpClientService;
        _configuration = configuration;
    }

    public async Task<TableNamesDto> GetTablesName(QueryParameters queryParameters)
    {
        return await _httpClientService.SendAsync<QueryParameters, TableNamesDto>(
            $"{_configuration["SqlServiceUrl"]}/api/ConsoleAppHub/all-tables-names", queryParameters, HttpMethod.Post);
    }
}