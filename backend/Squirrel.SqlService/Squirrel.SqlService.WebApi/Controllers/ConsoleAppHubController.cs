﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Squirrel.Core.BLL.Hubs;
using Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;
using Squirrel.SqlService.BLL.Models.ConsoleAppHub;

namespace Squirrel.SqlService.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConsoleAppHubController : ControllerBase
{
    private readonly IHubContext<ConsoleAppHub, IExecuteOnClientSide> _hubContext;

    public ConsoleAppHubController(IHubContext<ConsoleAppHub, IExecuteOnClientSide> hubContext)
    {
        _hubContext = hubContext;
    }


    // https://localhost:7244/api/ConsoleAppHub/getAllTablesNames
    [HttpPost("getAllTablesNames")]
    public async Task<ActionResult> GetAllTablesNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetAllTablesNamesAsync(queryParameters.ClientId);
        return NoContent();
    }

    // https://localhost:7244/api/ConsoleAppHub/getTableData
    [HttpPost("getTableData")]
    public async Task<ActionResult> GetTableDataAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetTableDataAsync(queryParameters.ClientId, queryParameters.FilterSchema, queryParameters.FilterName, queryParameters.FilterRowsCount);
        return NoContent();
    }

    // https://localhost:7244/api/ConsoleAppHub/getAllStoredProceduresNames
    [HttpPost("getAllStoredProceduresNames")]
    public async Task<ActionResult> GetAllStoredProceduresNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetAllStoredProceduresNamesAsync(queryParameters.ClientId);
        return NoContent();
    }

    // https://localhost:7244/api/ConsoleAppHub/getStoredProcedureDefinition
    [HttpPost("getStoredProcedureDefinition")]
    public async Task<ActionResult> GetStoredProcedureDefinitionAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetStoredProcedureDefinitionAsync(queryParameters.ClientId, queryParameters.FilterName);
        return NoContent();
    }

    // https://localhost:7244/api/ConsoleAppHub/getAllFunctionsNames
    [HttpPost("getAllFunctionsNames")]
    public async Task<ActionResult> GetAllFunctionsNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetAllFunctionsNamesAsync(queryParameters.ClientId);
        return NoContent();
    }

    // https://localhost:7244/api/ConsoleAppHub/getFunctionDefinition
    [HttpPost("getFunctionDefinition")]
    public async Task<ActionResult> GetFunctionDefinitionAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetFunctionDefinitionAsync(queryParameters.ClientId, queryParameters.FilterName);
        return NoContent();
    }

    // https://localhost:7244/api/ConsoleAppHub/getAllViewsNames
    [HttpPost("getAllViewsNames")]
    public async Task<ActionResult> GetAllViewsNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetAllViewsNamesAsync(queryParameters.ClientId);
        return NoContent();
    }

    // https://localhost:7244/api/ConsoleAppHub/getViewDefinition
    [HttpPost("getViewDefinition")]
    public async Task<ActionResult> GetViewDefinitionAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetViewDefinitionAsync(queryParameters.ClientId, queryParameters.FilterName);
        return NoContent();
    }

    // https://localhost:7244/api/ConsoleAppHub/getTableStructure
    [HttpPost("getTableStructure")]
    public async Task<ActionResult> GetTableStructureAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetTableStructureAsync(queryParameters.ClientId, queryParameters.FilterSchema, queryParameters.FilterName);
        return NoContent();
    }

    // https://localhost:7244/api/ConsoleAppHub/getTableChecksAndUniqueConstraints
    [HttpPost("getTableChecksAndUniqueConstraints")]
    public async Task<ActionResult> GetTableChecksAndUniqueConstraintsAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetTableChecksAndUniqueConstraintsAsync(queryParameters.ClientId, queryParameters.FilterSchema, queryParameters.FilterName);
        return NoContent();
    }

    // https://localhost:7244/api/ConsoleAppHub/getStoredProceduresWithDetail
    [HttpPost("getStoredProceduresWithDetail")]
    public async Task<ActionResult> GetStoredProceduresWithDetailAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetStoredProceduresWithDetailAsync(queryParameters.ClientId);
        return NoContent();
    }

    // https://localhost:7244/api/ConsoleAppHub/getFunctionsWithDetail
    [HttpPost("getFunctionsWithDetail")]
    public async Task<ActionResult> GetFunctionsWithDetailAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetFunctionsWithDetailAsync(queryParameters.ClientId);
        return NoContent();
    }

    // https://localhost:7244/api/ConsoleAppHub/getViewsWithDetail
    [HttpPost("getViewsWithDetail")]
    public async Task<ActionResult> GetViewsWithDetailAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetViewsWithDetailAsync(queryParameters.ClientId);
        return NoContent();
    }

    // https://localhost:7244/api/ConsoleAppHub/getUserDefinedTypesWithDefaultsAndRulesAndDefinition
    [HttpPost("getUserDefinedTypesWithDefaultsAndRulesAndDefinition")]
    public async Task<ActionResult> GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync(queryParameters.ClientId);
        return NoContent();
    }

    // https://localhost:7244/api/ConsoleAppHub/getUserDefinedTableTypes
    [HttpPost("getUserDefinedTableTypes")]
    public async Task<ActionResult> GetUserDefinedTableTypesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetUserDefinedTableTypesAsync(queryParameters.ClientId);
        return NoContent();
    }
}