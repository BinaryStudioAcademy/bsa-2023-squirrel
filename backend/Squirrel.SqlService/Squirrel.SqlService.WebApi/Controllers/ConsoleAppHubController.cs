using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Squirrel.ConsoleApp.Models;
using Squirrel.SqlService.BLL.Hubs;
using Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;
using Squirrel.SqlService.BLL.Models.ConsoleAppHub;
using Squirrel.SqlService.BLL.Models.DTO;
using Squirrel.SqlService.BLL.Models.DTO.Function;
using Squirrel.SqlService.BLL.Models.DTO.Procedure;
using Squirrel.SqlService.BLL.Models.DTO.Shared;
using Squirrel.SqlService.BLL.Services.ConsoleAppHub;

namespace Squirrel.SqlService.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConsoleAppHubController : ControllerBase
{
    private readonly IHubContext<ConsoleAppHub, IExecuteOnClientSide> _hubContext;
    private readonly ResultObserver _resultObserver;
    private readonly IMapper _mapper;
    private readonly (Guid queryId, TaskCompletionSource<QueryResultTable> tcs) _queryParameters;

    public ConsoleAppHubController(IHubContext<ConsoleAppHub, IExecuteOnClientSide> hubContext,
        ResultObserver resultObserver, IMapper mapper)
    {
        _hubContext = hubContext;
        _resultObserver = resultObserver;
        _mapper = mapper;
        _queryParameters = RegisterQuery();
    }

    private (Guid queryId, TaskCompletionSource<QueryResultTable> tcs) RegisterQuery()
    {
        var queryId = Guid.NewGuid();
        var tcs = _resultObserver.Register(queryId);
        return (queryId, tcs);
    }

    // https://localhost:7244/api/ConsoleAppHub/getAllTablesNames
    [HttpPost("getAllTablesNames")]
    public async Task<ActionResult<TableNamesDto>> GetAllTablesNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetAllTablesNamesAsync(_queryParameters.queryId);
        return Ok(_mapper.Map<TableNamesDto>(await _queryParameters.tcs.Task));
    }

    // https://localhost:7244/api/ConsoleAppHub/getTableData
    [HttpPost("getTableData")]
    public async Task<ActionResult<TableDataDto>> GetTableDataAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetTableDataAsync(_queryParameters.queryId,
            queryParameters.FilterSchema, queryParameters.FilterName, queryParameters.FilterRowsCount);
        return Ok(_mapper.Map<TableDataDto>(await _queryParameters.tcs.Task));
    }

    // https://localhost:7244/api/ConsoleAppHub/getAllStoredProceduresNames
    [HttpPost("getAllStoredProceduresNames")]
    public async Task<ActionResult<ProcedureNamesDto>> GetAllStoredProceduresNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId)
            .GetAllStoredProceduresNamesAsync(_queryParameters.queryId);
        return Ok(await _queryParameters.tcs.Task);
    }

    // https://localhost:7244/api/ConsoleAppHub/getStoredProcedureDefinition
    [HttpPost("getStoredProcedureDefinition")]
    public async Task<ActionResult> GetStoredProcedureDefinitionAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId)
            .GetStoredProcedureDefinitionAsync(_queryParameters.queryId, queryParameters.FilterSchema,
                queryParameters.FilterName);
        return Ok(await _queryParameters.tcs.Task);
    }

    // https://localhost:7244/api/ConsoleAppHub/getAllFunctionsNames
    [HttpPost("getAllFunctionsNames")]
    public async Task<ActionResult> GetAllFunctionsNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetAllFunctionsNamesAsync(_queryParameters.queryId);
        return Ok(await _queryParameters.tcs.Task);
    }

    // https://localhost:7244/api/ConsoleAppHub/getFunctionDefinition
    [HttpPost("getFunctionDefinition")]
    public async Task<ActionResult<RoutineDefinitionDto>> GetFunctionDefinitionAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId)
            .GetFunctionDefinitionAsync(_queryParameters.queryId, queryParameters.FilterSchema,
                queryParameters.FilterName);
        return Ok(_mapper.Map<RoutineDefinitionDto>(await _queryParameters.tcs.Task));
    }

    // https://localhost:7244/api/ConsoleAppHub/getAllViewsNames
    [HttpPost("getAllViewsNames")]
    public async Task<ActionResult> GetAllViewsNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetAllViewsNamesAsync(_queryParameters.queryId);
        return Ok(await _queryParameters.tcs.Task);
    }

    // https://localhost:7244/api/ConsoleAppHub/getViewDefinition
    [HttpPost("getViewDefinition")]
    public async Task<ActionResult<RoutineDefinitionDto>> GetViewDefinitionAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId)
            .GetViewDefinitionAsync(_queryParameters.queryId, queryParameters.FilterName);
        return Ok(_mapper.Map<RoutineDefinitionDto>(await _queryParameters.tcs.Task));
    }

    // https://localhost:7244/api/ConsoleAppHub/getTableStructure
    [HttpPost("getTableStructure")]
    public async Task<ActionResult<TableStructureDto>> GetTableStructureAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetTableStructureAsync(_queryParameters.queryId,
            queryParameters.FilterSchema, queryParameters.FilterName);
        return Ok(_mapper.Map<TableStructureDto>(await _queryParameters.tcs.Task));
    }

    // https://localhost:7244/api/ConsoleAppHub/getTableChecksAndUniqueConstraints
    [HttpPost("getTableChecksAndUniqueConstraints")]
    public async Task<ActionResult<TableConstraintsDto>> GetTableChecksAndUniqueConstraintsAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId)
            .GetTableChecksAndUniqueConstraintsAsync(_queryParameters.queryId, queryParameters.FilterSchema,
                queryParameters.FilterName);
        return Ok(_mapper.Map<TableConstraintsDto>(await _queryParameters.tcs.Task));
    }

    // https://localhost:7244/api/ConsoleAppHub/getStoredProceduresWithDetail
    [HttpPost("getStoredProceduresWithDetail")]
    public async Task<ActionResult<ProcedureDetailsDto>> GetStoredProceduresWithDetailAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId)
            .GetStoredProceduresWithDetailAsync(_queryParameters.queryId);
        return Ok(_mapper.Map<ProcedureDetailsDto>(await _queryParameters.tcs.Task));
    }

    // https://localhost:7244/api/ConsoleAppHub/getFunctionsWithDetail
    [HttpPost("getFunctionsWithDetail")]
    public async Task<ActionResult<FunctionDetailsDto>> GetFunctionsWithDetailAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetFunctionsWithDetailAsync(_queryParameters.queryId);
        return Ok(_mapper.Map<FunctionDetailsDto>(await _queryParameters.tcs.Task));
    }

    // https://localhost:7244/api/ConsoleAppHub/getViewsWithDetail
    [HttpPost("getViewsWithDetail")]
    public async Task<ActionResult> GetViewsWithDetailAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetViewsWithDetailAsync(_queryParameters.queryId);
        return Ok(await _queryParameters.tcs.Task);
    }

    // https://localhost:7244/api/ConsoleAppHub/getUserDefinedTypesWithDefaultsAndRulesAndDefinition
    [HttpPost("getUserDefinedTypesWithDefaultsAndRulesAndDefinition")]
    public async Task<ActionResult> GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync(
        [FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId)
            .GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync(_queryParameters.queryId);
        return Ok(await _queryParameters.tcs.Task);
    }

    // https://localhost:7244/api/ConsoleAppHub/getUserDefinedTableTypes
    [HttpPost("getUserDefinedTableTypes")]
    public async Task<ActionResult> GetUserDefinedTableTypesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId)
            .GetUserDefinedTableTypesAsync(_queryParameters.queryId);
        return Ok(await _queryParameters.tcs.Task);
    }
}