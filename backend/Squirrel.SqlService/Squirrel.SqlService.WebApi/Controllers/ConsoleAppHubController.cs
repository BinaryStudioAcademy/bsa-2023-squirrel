﻿using AutoMapper;
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
    
    [HttpPost("all-tables-names")]
    public async Task<ActionResult<TableNamesDto>> GetAllTablesNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetAllTablesNamesAsync(_queryParameters.queryId);
        return Ok(_mapper.Map<TableNamesDto>(await _queryParameters.tcs.Task));
    }
    
    [HttpPost("table-data")]
    public async Task<ActionResult<TableDataDto>> GetTableDataAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetTableDataAsync(_queryParameters.queryId,
            queryParameters.FilterSchema, queryParameters.FilterName, queryParameters.FilterRowsCount);
        return Ok(_mapper.Map<TableDataDto>(await _queryParameters.tcs.Task));
    }
    
    [HttpPost("all-stored-procedures-names")]
    public async Task<ActionResult<ProcedureNamesDto>> GetAllStoredProceduresNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId)
            .GetAllStoredProceduresNamesAsync(_queryParameters.queryId);
        return Ok(await _queryParameters.tcs.Task);
    }
    
    [HttpPost("stored-procedure-definition")]
    public async Task<ActionResult> GetStoredProcedureDefinitionAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId)
            .GetStoredProcedureDefinitionAsync(_queryParameters.queryId, queryParameters.FilterSchema,
                queryParameters.FilterName);
        return Ok(await _queryParameters.tcs.Task);
    }
    
    [HttpPost("all-functions-names")]
    public async Task<ActionResult> GetAllFunctionsNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetAllFunctionsNamesAsync(_queryParameters.queryId);
        return Ok(await _queryParameters.tcs.Task);
    }
    
    [HttpPost("function-definition")]
    public async Task<ActionResult<RoutineDefinitionDto>> GetFunctionDefinitionAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId)
            .GetFunctionDefinitionAsync(_queryParameters.queryId, queryParameters.FilterSchema,
                queryParameters.FilterName);
        return Ok(_mapper.Map<RoutineDefinitionDto>(await _queryParameters.tcs.Task));
    }
    
    [HttpPost("all-views-names")]
    public async Task<ActionResult> GetAllViewsNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetAllViewsNamesAsync(_queryParameters.queryId);
        return Ok(await _queryParameters.tcs.Task);
    }
    
    [HttpPost("view-definition")]
    public async Task<ActionResult<RoutineDefinitionDto>> GetViewDefinitionAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId)
            .GetViewDefinitionAsync(_queryParameters.queryId, queryParameters.FilterName);
        return Ok(_mapper.Map<RoutineDefinitionDto>(await _queryParameters.tcs.Task));
    }
    
    [HttpPost("table-structure")]
    public async Task<ActionResult<TableStructureDto>> GetTableStructureAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetTableStructureAsync(_queryParameters.queryId,
            queryParameters.FilterSchema, queryParameters.FilterName);
        return Ok(_mapper.Map<TableStructureDto>(await _queryParameters.tcs.Task));
    }

    [HttpPost("tTable-checks-and-unique-constraints")]
    public async Task<ActionResult<TableConstraintsDto>> GetTableChecksAndUniqueConstraintsAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId)
            .GetTableChecksAndUniqueConstraintsAsync(_queryParameters.queryId, queryParameters.FilterSchema,
                queryParameters.FilterName);
        return Ok(_mapper.Map<TableConstraintsDto>(await _queryParameters.tcs.Task));
    }
    
    [HttpPost("stored-procedures-with-detail")]
    public async Task<ActionResult<ProcedureDetailsDto>> GetStoredProceduresWithDetailAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId)
            .GetStoredProceduresWithDetailAsync(_queryParameters.queryId);
        return Ok(_mapper.Map<ProcedureDetailsDto>(await _queryParameters.tcs.Task));
    }

    [HttpPost("functions-with-detail")]
    public async Task<ActionResult<FunctionDetailsDto>> GetFunctionsWithDetailAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetFunctionsWithDetailAsync(_queryParameters.queryId);
        return Ok(_mapper.Map<FunctionDetailsDto>(await _queryParameters.tcs.Task));
    }
    
    [HttpPost("views-with-detail")]
    public async Task<ActionResult> GetViewsWithDetailAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetViewsWithDetailAsync(_queryParameters.queryId);
        return Ok(await _queryParameters.tcs.Task);
    }
    
    [HttpPost("user-defined-types-with-defaults-and-rules-and-definition")]
    public async Task<ActionResult> GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync(
        [FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId)
            .GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync(_queryParameters.queryId);
        return Ok(await _queryParameters.tcs.Task);
    }
    
    [HttpPost("user-defined-table-types")]
    public async Task<ActionResult> GetUserDefinedTableTypesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId)
            .GetUserDefinedTableTypesAsync(_queryParameters.queryId);
        return Ok(await _queryParameters.tcs.Task);
    }
    
    [HttpPost("db-connect")]
    public async Task<ActionResult> ConnectToDb([FromBody] RemoteConnect remoteConnect)
    {
        await _hubContext.Clients.User(remoteConnect.ClientId)
            .RemoteConnectAsync(_queryParameters.queryId, remoteConnect.DbConnection);
        return Ok(await _queryParameters.tcs.Task);
    }
}