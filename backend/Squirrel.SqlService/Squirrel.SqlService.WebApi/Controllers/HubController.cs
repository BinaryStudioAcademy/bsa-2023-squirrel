using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Squirrel.Core.BLL.Hubs;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.SqlService.BLL.Models.SquirrelHub;

namespace Squirrel.SqlService.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HubController : ControllerBase
{
    private readonly IHubContext<SquirrelHub, ISquirrelHubToSend> _hubContext;

    public HubController(IHubContext<SquirrelHub, ISquirrelHubToSend> hubContext)
    {
        _hubContext = hubContext;
    }


    [HttpPost("testExecuteQuery")]
    public async Task<ActionResult>TestExecuteQuery([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.All.TestExecuteQueryAsync(queryParameters.ClientId, queryParameters.FilterName, queryParameters.FilterRowsCount);
        return NoContent();
    }


    [HttpPost("getAllTablesNames")]
    public async Task<ActionResult> GetAllTablesNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.All.GetAllTablesNamesAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getTableData")]
    public async Task<ActionResult> GetTableDataAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.All.GetTableDataAsync(queryParameters.ClientId, queryParameters.FilterName, queryParameters.FilterRowsCount);
        return NoContent();
    }

    [HttpPost("getAllStoredProceduresNames")]
    public async Task<ActionResult> GetAllStoredProceduresNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.All.GetAllStoredProceduresNamesAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getStoredProcedureDefinition")]
    public async Task<ActionResult> GetStoredProcedureDefinitionAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.All.GetStoredProcedureDefinitionAsync(queryParameters.ClientId, queryParameters.FilterName);
        return NoContent();
    }

    [HttpPost("getAllFunctionsNames")]
    public async Task<ActionResult> GetAllFunctionsNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.All.GetAllFunctionsNamesAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getFunctionDefinition")]
    public async Task<ActionResult> GetFunctionDefinitionAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.All.GetFunctionDefinitionAsync(queryParameters.ClientId, queryParameters.FilterName);
        return NoContent();
    }

    [HttpPost("getAllViewsNames")]
    public async Task<ActionResult> GetAllViewsNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.All.GetAllViewsNamesAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getViewDefinition")]
    public async Task<ActionResult> GetViewDefinitionAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.All.GetViewDefinitionAsync(queryParameters.ClientId, queryParameters.FilterName);
        return NoContent();
    }

    [HttpPost("getDbTablesStructure")]
    public async Task<ActionResult> GetDbTablesStructureAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.All.GetDbTablesStructureAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getDbTablesCheckAndUniqueConstraints")]
    public async Task<ActionResult> GetDbTablesCheckAndUniqueConstraintsAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.All.GetDbTablesCheckAndUniqueConstraintsAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getStoredProceduresWithDetail")]
    public async Task<ActionResult> GetStoredProceduresWithDetailAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.All.GetStoredProceduresWithDetailAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getFunctionsWithDetail")]
    public async Task<ActionResult> GetFunctionsWithDetailAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.All.GetFunctionsWithDetailAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getViewsWithDetail")]
    public async Task<ActionResult> GetViewsWithDetailAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.All.GetViewsWithDetailAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getUserDefinedTypesWithDefaultsAndRulesAndDefinition")]
    public async Task<ActionResult> GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.All.GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getUserDefinedTableTypes")]
    public async Task<ActionResult> GetUserDefinedTableTypesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.All.GetUserDefinedTableTypesAsync(queryParameters.ClientId);
        return NoContent();
    }
}