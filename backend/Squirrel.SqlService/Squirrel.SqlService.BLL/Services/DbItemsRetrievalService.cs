using AutoMapper;
using Microsoft.Extensions.Configuration;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.Interfaces;
using Squirrel.SqlService.BLL.Interfaces;
using Squirrel.SqlService.BLL.Models.ConsoleAppHub;
using Squirrel.SqlService.BLL.Models.DTO;
using Squirrel.SqlService.BLL.Models.DTO.Function;
using Squirrel.SqlService.BLL.Models.DTO.Procedure;

namespace Squirrel.SqlService.BLL.Services;

public class DbItemsRetrievalService : IDbItemsRetrievalService
{
    private readonly IHttpClientService _httpClientService;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    private readonly string SqlServiceRoute;

    public DbItemsRetrievalService(IHttpClientService httpClientService, IMapper mapper, IConfiguration configuration)
    {
        _httpClientService = httpClientService;
        _mapper = mapper;
        _configuration = configuration;

        SqlServiceRoute = _configuration["SqlServiceRoute"];
    }

    public async Task<DbStructureDto> GetAllDbStructureAsync(Guid clientId)
    {
        var structuresResult = await GetAllTableStructuresAsync(clientId);
        var constraintsResult = await GetAllTableConstraintsAsync(clientId);
        var functionDetailsResult = await GetAllFunctionDetailsAsync(clientId);
        var proceduresDetailsResult = await GetAllProcedureDetailsAsync(clientId);

        var dbStructureResult = new DbStructureDto()
        {
            //TableStructures = structuresResult,
            Constraints = constraintsResult,
            FunctionDetails = functionDetailsResult,
            ProcedureDetails = proceduresDetailsResult
        };

        return dbStructureResult;
    }

    public async Task<TableNamesDto> GetTableNamesAsync(Guid clientId)
    {
        var tableNamesRetriveRequest = new QueryParameters()
        {
            ClientId = clientId.ToString(),
        };

        var tableNamesResult = await _httpClientService
            .PostAsync<QueryParameters, TableNamesDto>
            ($"{SqlServiceRoute}/api/ConsoleAppHub/getAllTablesNames", tableNamesRetriveRequest);

        return tableNamesResult;
    }

    public async Task<ICollection<TableStructureDto>> GetAllTableStructuresAsync(Guid clientId)
    {
        var tableNames = await GetTableNamesAsync(clientId);

        List<TableStructureDto> tablesStructuresResults = new();

        foreach (var table in tableNames.Tables)
        {
            var tableStructureRetriveRequest = new QueryParameters()
            {
                ClientId = clientId.ToString(),
                FilterSchema = table.Schema,
                FilterName = table.Name,
            };

            var tableNamesResult = await _httpClientService
                .PostAsync<QueryParameters, TableStructureDto>
                ($"{SqlServiceRoute}/api/ConsoleAppHub/getTableStructure", tableStructureRetriveRequest);

            tablesStructuresResults.Add(tableNamesResult);
        }

        return tablesStructuresResults;
    }

    public async Task<ICollection<TableConstraintsDto>> GetAllTableConstraintsAsync(Guid clientId)
    {
        var tableNames = await GetTableNamesAsync(clientId);

        List<TableConstraintsDto> tableConstraintsResult = new();

        foreach (var table in tableNames.Tables)
        {
            var tableConstraintsRetriveRequest = new QueryParameters()
            {
                ClientId = clientId.ToString(),
                FilterName = table.Name,
                FilterSchema = table.Schema,
            };

            var tableNamesResult = await _httpClientService
                .PostAsync<QueryParameters, TableConstraintsDto>
                ($"{SqlServiceRoute}/api/ConsoleAppHub/getTableChecksAndUniqueConstraints", tableConstraintsRetriveRequest);

            tableConstraintsResult.Add(tableNamesResult);
        }

        return tableConstraintsResult;
    }

    public async Task<FunctionNamesDto> GetFunctionsNamesAsync(Guid clientId)
    {
        var functionNamesRetriveRequest = new QueryParameters()
        {
            ClientId = clientId.ToString(),
        };

        var functionNamesResult = await _httpClientService
            .PostAsync<QueryParameters, FunctionNamesDto>
            ($"{SqlServiceRoute}/api/ConsoleAppHub/getAllFunctionsNames", functionNamesRetriveRequest);

        return functionNamesResult;
    }

    public async Task<FunctionDetailsDto> GetAllFunctionDetailsAsync(Guid clientId)
    {
        var functionDetailRetriveRequest = new QueryParameters()
        {
            ClientId = clientId.ToString(),
        };

        var functionDetailResult = await _httpClientService
            .PostAsync<QueryParameters, FunctionDetailsDto>
            ($"{SqlServiceRoute}/api/ConsoleAppHub/getFunctionsWithDetail", functionDetailRetriveRequest);


        return functionDetailResult;
    }

    public async Task<ProcedureNamesDto> GetProceduresNamesAsync(Guid clientId)
    {
        var proceduresNamesRetriveRequest = new QueryParameters()
        {
            ClientId = clientId.ToString(),
        };

        var proceduresNamesResult = await _httpClientService
            .PostAsync<QueryParameters, ProcedureNamesDto>
            ($"{SqlServiceRoute}/api/ConsoleAppHub/getAllStoredProceduresNames", proceduresNamesRetriveRequest);

        return proceduresNamesResult;
    }

    public async Task<ProcedureDetailsDto> GetAllProcedureDetailsAsync(Guid clientId)
    {
        var procedureDetailsRetriveRequest = new QueryParameters()
        {
            ClientId = clientId.ToString(),
        };

        var procedureDetailResult = await _httpClientService
            .PostAsync<QueryParameters, ProcedureDetailsDto>
            ($"{SqlServiceRoute}/api/ConsoleAppHub/getStoredProceduresWithDetail", procedureDetailsRetriveRequest);

        return procedureDetailResult;
    }

    public async Task<ICollection<DatabaseItem>> GetAllItemsAsync(Guid clientId)
    {
        List<DatabaseItem> items = new();

        var tableNames = await GetTableNamesAsync(clientId);
        var functionNames = await GetFunctionsNamesAsync(clientId);
        var procedureNames = await GetProceduresNamesAsync(clientId);

        items.AddRange(_mapper.Map<List<DatabaseItem>>(tableNames.Tables));
        items.AddRange(_mapper.Map<List<DatabaseItem>>(functionNames.Functions));
        items.AddRange(_mapper.Map<List<DatabaseItem>>(procedureNames.Procedures));

        return items;
    }
}
