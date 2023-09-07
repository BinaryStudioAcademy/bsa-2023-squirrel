using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Squirrel.Core.BLL.Hubs;
using Squirrel.SqlService.BLL.Interfaces.ConsoleAppHub;
using Squirrel.SqlService.BLL.Models.ConsoleAppHub;

namespace Squirrel.SqlService.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HubController : ControllerBase
{
    private readonly IHubContext<ConsoleAppHub, IExecuteOnClientSide> _hubContext;

    public HubController(IHubContext<ConsoleAppHub, IExecuteOnClientSide> hubContext)
    {
        _hubContext = hubContext;
    }


    [HttpPost("getAllTablesNames")]
    public async Task<ActionResult> GetAllTablesNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetAllTablesNamesAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getTableData")]
    public async Task<ActionResult> GetTableDataAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetTableDataAsync(queryParameters.ClientId, queryParameters.FilterName, queryParameters.FilterRowsCount);
        return NoContent();
    }

    [HttpPost("getAllStoredProceduresNames")]
    public async Task<ActionResult> GetAllStoredProceduresNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetAllStoredProceduresNamesAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getStoredProcedureDefinition")]
    public async Task<ActionResult> GetStoredProcedureDefinitionAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetStoredProcedureDefinitionAsync(queryParameters.ClientId, queryParameters.FilterName);
        return NoContent();
    }

    [HttpPost("getAllFunctionsNames")]
    public async Task<ActionResult> GetAllFunctionsNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetAllFunctionsNamesAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getFunctionDefinition")]
    public async Task<ActionResult> GetFunctionDefinitionAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetFunctionDefinitionAsync(queryParameters.ClientId, queryParameters.FilterName);
        return NoContent();
    }

    [HttpPost("getAllViewsNames")]
    public async Task<ActionResult> GetAllViewsNamesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetAllViewsNamesAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getViewDefinition")]
    public async Task<ActionResult> GetViewDefinitionAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetViewDefinitionAsync(queryParameters.ClientId, queryParameters.FilterName);
        return NoContent();
    }

    [HttpPost("getDbTablesStructure")]
    public async Task<ActionResult> GetDbTablesStructureAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetDbTablesStructureAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getDbTablesCheckAndUniqueConstraints")]
    public async Task<ActionResult> GetDbTablesCheckAndUniqueConstraintsAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetDbTablesCheckAndUniqueConstraintsAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getStoredProceduresWithDetail")]
    public async Task<ActionResult> GetStoredProceduresWithDetailAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetStoredProceduresWithDetailAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getFunctionsWithDetail")]
    public async Task<ActionResult> GetFunctionsWithDetailAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetFunctionsWithDetailAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getViewsWithDetail")]
    public async Task<ActionResult> GetViewsWithDetailAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetViewsWithDetailAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getUserDefinedTypesWithDefaultsAndRulesAndDefinition")]
    public async Task<ActionResult> GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionAsync(queryParameters.ClientId);
        return NoContent();
    }

    [HttpPost("getUserDefinedTableTypes")]
    public async Task<ActionResult> GetUserDefinedTableTypesAsync([FromBody] QueryParameters queryParameters)
    {
        await _hubContext.Clients.User(queryParameters.ClientId).GetUserDefinedTableTypesAsync(queryParameters.ClientId);
        return NoContent();
    }
}