using Microsoft.Extensions.Configuration;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Shared.DTO.ConsoleAppHub;
using Squirrel.Shared.DTO.Table;

namespace Squirrel.Core.BLL.Services;

public class TableService : ITableService
{
    private const string AllTableNamesRoutePrefix = "/api/ConsoleAppHub/all-tables-names";
    private const string TableStructureRoutePrefix = "/api/ConsoleAppHub/table-structure";

    private readonly IHttpClientService _httpClientService;
    private readonly IConfiguration _configuration;

    public TableService(IHttpClientService httpClientService, IConfiguration configuration)
    {
        _httpClientService = httpClientService;
        _configuration = configuration;
    }

    public async Task<TableNamesDto> GetTablesNameAsync(QueryParameters queryParameters)
    {
        return await _httpClientService.SendAsync<QueryParameters, TableNamesDto>(
            $"{_configuration[BaseService.SqlServiceUrlSection]}{AllTableNamesRoutePrefix}", queryParameters, HttpMethod.Post);
    }

    public Task<TableStructureDto> GetTablesStructureAsync(QueryParameters queryParameters)
    {
        throw new NotImplementedException();
    }

    public async Task<TableStructureDto> GetTableStructureAsync(QueryParameters queryParameters)
    {
        return await _httpClientService.SendAsync<QueryParameters, TableStructureDto>(
            $"{_configuration[BaseService.SqlServiceUrlSection]}{TableStructureRoutePrefix}", queryParameters, HttpMethod.Post);
    }
}