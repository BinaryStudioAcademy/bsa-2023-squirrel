using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Squirrel.ConsoleApp.Models;
using Squirrel.Shared.DTO;
using Squirrel.Shared.DTO.ConsoleAppHub;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.DTO.Function;
using Squirrel.Shared.DTO.Procedure;
using Squirrel.Shared.DTO.Table;
using Squirrel.Shared.DTO.UserDefinedType.DataType;
using Squirrel.Shared.DTO.UserDefinedType.TableType;
using Squirrel.Shared.DTO.View;
using Squirrel.SqlService.BLL.Interfaces;
using Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;

namespace Squirrel.SqlService.BLL.Services;

public class DbItemsRetrievalService : IDbItemsRetrievalService
{
    private readonly IHubContext<Hubs.ConsoleAppHub, IExecuteOnClientSide> _hubContext;
    private readonly IResultObserver _resultObserver;
    private readonly IMapper _mapper;
    private readonly ICreateTableScriptService _createTableScriptService;

    public DbItemsRetrievalService(IHubContext<Hubs.ConsoleAppHub, IExecuteOnClientSide> hubContext,
        IResultObserver resultObserver, IMapper mapper, ICreateTableScriptService createTableScriptService)
    {
        _hubContext = hubContext;
        _resultObserver = resultObserver;
        _mapper = mapper;
        _createTableScriptService = createTableScriptService;
    }

    private (Guid queryId, TaskCompletionSource<QueryResultTable> tcs) RegisterQuery()
    {
        var queryId = Guid.NewGuid();
        var tcs = _resultObserver.Register(queryId);
        return (queryId, tcs);
    }

    public async Task<DbStructureDto> GetAllDbStructureAsync(Guid clientId)
    {
        var proceduresDetailsResult = await GetAllProcedureDetailsAsync(clientId);
        var structuresResult = await GetAllTableStructuresAsync(clientId);
        var constraintsResult = await GetAllTableConstraintsAsync(clientId);
        var functionDetailsResult = await GetAllFunctionDetailsAsync(clientId);
        var viewsDetailsResult = await GetAllViewDetailsAsync(clientId);
        var udtDataTypeDetailsResult = await GetAllUdtDataTypeDetails(clientId);
        var udtTableTypeDetailsResult = await GetAllUdtTableTypeDetails(clientId);

        var dbStructureResult = new DbStructureDto
        {
            DbTableStructures = structuresResult.ToList(),
            DbConstraints = constraintsResult.ToList(),
            DbFunctionDetails = functionDetailsResult,
            DbProcedureDetails = proceduresDetailsResult,
            DbViewsDetails = viewsDetailsResult,
            DbUserDefinedDataTypeDetailsDto = udtDataTypeDetailsResult,
            DbUserDefinedTableTypeDetailsDto = udtTableTypeDetailsResult
        };

        return dbStructureResult;
    }

    public async Task<ICollection<DatabaseItem>> GetAllItemsAsync(Guid clientId)
    {
        List<DatabaseItem> items = new();

        var tableNames = await GetTableNamesAsync(clientId);
        var viewNames = await GetViewNamesAsync(clientId);
        var functionNames = await GetFunctionsNamesAsync(clientId);
        var procedureNames = await GetProceduresNamesAsync(clientId);

        items.AddRange(_mapper.Map<List<DatabaseItem>>(tableNames.Tables));
        items.AddRange(_mapper.Map<List<DatabaseItem>>(viewNames.Views));
        items.AddRange(_mapper.Map<List<DatabaseItem>>(functionNames.Functions));
        items.AddRange(_mapper.Map<List<DatabaseItem>>(procedureNames.Procedures));

        return items;
    }

    public async Task<TableNamesDto> GetTableNamesAsync(Guid clientId)
    {
        var queryParametersRequest = RegisterQuery();

        await _hubContext.Clients.User(clientId.ToString()).GetAllTablesNamesAsync(queryParametersRequest.queryId);
        var tableNamesResult = _mapper.Map<TableNamesDto>(await queryParametersRequest.tcs.Task);

        return tableNamesResult;
    }

    public async Task<ICollection<TableStructureDto>> GetAllTableStructuresAsync(Guid clientId)
    {
        var tableNames = await GetTableNamesAsync(clientId);

        List<TableStructureDto> tablesStructuresResults = new();
        List<string> fkConstraintScripts = new();

        foreach (var table in tableNames.Tables)
        {
            var queryParameters = new QueryParameters()
            {
                ClientId = clientId.ToString(),
                FilterSchema = table.Schema,
                FilterName = table.Name,
            };

            var queryParametersRequest = RegisterQuery();

            await _hubContext.Clients.User(queryParameters.ClientId)
                .GetTableStructureAsync(queryParametersRequest.queryId, queryParameters.FilterSchema, queryParameters.FilterName);

            var tableStructureResult = _mapper.Map<TableStructureDto>(await queryParametersRequest.tcs.Task);

            var partialDefinition = _createTableScriptService.GenerateCreateTableScript(tableStructureResult.Schema,
                tableStructureResult.Name, tableStructureResult, fkConstraintScripts);
            
            tableStructureResult.Definition = _createTableScriptService.ConcatForeignKeysConstraintScripts(
                new StringBuilder(partialDefinition), fkConstraintScripts);;

            tablesStructuresResults.Add(tableStructureResult);
        }

        return tablesStructuresResults;
    }

    public async Task<ICollection<TableConstraintsDto>> GetAllTableConstraintsAsync(Guid clientId)
    {
        var tableNames = await GetTableNamesAsync(clientId);

        List<TableConstraintsDto> tableConstraintsResult = new();

        foreach (var table in tableNames.Tables)
        {
            var queryParameters = new QueryParameters()
            {
                ClientId = clientId.ToString(),
                FilterName = table.Name,
                FilterSchema = table.Schema,
            };

            var queryParametersRequest = RegisterQuery();

            await _hubContext.Clients.User(queryParameters.ClientId)
                .GetTableChecksAndUniqueConstraintsAsync(queryParametersRequest.queryId, queryParameters.FilterSchema, queryParameters.FilterName);

            var tableConstraintResult = _mapper.Map<TableConstraintsDto>(await queryParametersRequest.tcs.Task);

            if (tableConstraintResult.Constraints.Any())
            {
                tableConstraintsResult.Add(tableConstraintResult);
            }
        }

        return tableConstraintsResult;
    }

    public async Task<FunctionNamesDto> GetFunctionsNamesAsync(Guid clientId)
    {
        var queryParametersRequest = RegisterQuery();

        await _hubContext.Clients.User(clientId.ToString()).GetAllFunctionsNamesAsync(queryParametersRequest.queryId);
        var functionNamesResult = _mapper.Map<FunctionNamesDto>(await queryParametersRequest.tcs.Task);

        return functionNamesResult;
    }

    public async Task<FunctionDetailsDto> GetAllFunctionDetailsAsync(Guid clientId)
    {
        var queryParametersRequest = RegisterQuery();

        await _hubContext.Clients.User(clientId.ToString()).GetFunctionsWithDetailAsync(queryParametersRequest.queryId);
        var functionDetailResult = _mapper.Map<FunctionDetailsDto>(await queryParametersRequest.tcs.Task);

        return functionDetailResult;
    }

    public async Task<ProcedureNamesDto> GetProceduresNamesAsync(Guid clientId)
    {
        var queryParametersRequest = RegisterQuery();

        await _hubContext.Clients.User(clientId.ToString())
            .GetAllStoredProceduresNamesAsync(queryParametersRequest.queryId);
        var proceduresNamesResult = _mapper.Map<ProcedureNamesDto>(await queryParametersRequest.tcs.Task);

        return proceduresNamesResult;
    }

    public async Task<ProcedureDetailsDto> GetAllProcedureDetailsAsync(Guid clientId)
    {
        var queryParametersRequest = RegisterQuery();

        await _hubContext.Clients.User(clientId.ToString())
            .GetStoredProceduresWithDetailAsync(queryParametersRequest.queryId);
        var procedureDetailResult = _mapper.Map<ProcedureDetailsDto>(await queryParametersRequest.tcs.Task);

        return procedureDetailResult;
    }

    public async Task<ViewNamesDto> GetViewNamesAsync(Guid clientId)
    {
        var queryParametersRequest = RegisterQuery();

        await _hubContext.Clients.User(clientId.ToString()).GetAllViewsNamesAsync(queryParametersRequest.queryId);
        var viewsNamesResult = _mapper.Map<ViewNamesDto>(await queryParametersRequest.tcs.Task);

        return viewsNamesResult;
    }

    public async Task<ViewDetailsDto> GetAllViewDetailsAsync(Guid clientId)
    {
        var queryParametersRequest = RegisterQuery();

        await _hubContext.Clients.User(clientId.ToString()).GetViewsWithDetailAsync(queryParametersRequest.queryId);
        var viewDetailResult = _mapper.Map<ViewDetailsDto>(await queryParametersRequest.tcs.Task);

        return viewDetailResult;
    }

    public async Task<UserDefinedDataTypeDetailsDto> GetAllUdtDataTypeDetails(Guid clientId)
    {
        var queryParametersRequest = RegisterQuery();

        await _hubContext.Clients.User(clientId.ToString())
            .GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync(queryParametersRequest.queryId);
        var udtDataTypeDetailsResult = _mapper.Map<UserDefinedDataTypeDetailsDto>(await queryParametersRequest.tcs.Task);

        return udtDataTypeDetailsResult;
    }

    public async Task<UserDefinedTables> GetAllUdtTableTypeDetails(Guid clientId)
    {
        var queryParametersRequest = RegisterQuery();
        
        await _hubContext.Clients.User(clientId.ToString())
            .GetUserDefinedTableTypesAsync(queryParametersRequest.queryId);

        var udtTableTypeDetails = _mapper.Map<UserDefinedTables>(await queryParametersRequest.tcs.Task);

        return udtTableTypeDetails;
    }
}
