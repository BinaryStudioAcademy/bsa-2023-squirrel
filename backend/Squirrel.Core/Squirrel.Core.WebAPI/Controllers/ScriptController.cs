using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squirrel.ConsoleApp.Models;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.Script;

namespace Squirrel.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class ScriptController : ControllerBase
{
    private readonly IScriptService _scriptService;
    private readonly IUserIdGetter _userIdGetter;

    public ScriptController(IScriptService scriptService, IUserIdGetter userIdGetter)
    {
        _scriptService = scriptService;
        _userIdGetter = userIdGetter;
    }

    [HttpGet("{projectId}/all")]
    public async Task<ActionResult<ICollection<ScriptDto>>> GetAllScriptsAsync(int projectId)
    {
        return Ok(await _scriptService.GetAllScriptsAsync(projectId));
    }

    [HttpPost]
    public async Task<ActionResult<ScriptDto>> CreateScriptAsync(CreateScriptDto dto)
    {
        return Ok(await _scriptService.CreateScriptAsync(dto, _userIdGetter.GetCurrentUserId()));
    }

    [HttpPut]
    public async Task<ActionResult<ScriptDto>> UpdateScriptAsync(ScriptDto dto)
    {
        return Ok(await _scriptService.UpdateScriptAsync(dto, _userIdGetter.GetCurrentUserId()));
    }
    
    [HttpDelete("{scriptId}")]
    public async Task<ActionResult> DeleteScriptAsync(int scriptId)
    {
        await _scriptService.DeleteScriptAsync(scriptId);
        return NoContent();
    }

    /// <summary>
    /// Find errors and format provided SQL script
    /// </summary>
    [HttpPut("format")]
    public async Task<ActionResult<ScriptContentDto>> GetFormattedSqlAsync([FromBody] InboundScriptDto inboundScriptDto)
    {
        return Ok(await _scriptService.GetFormattedSqlAsync(inboundScriptDto));
    }

    /// <summary>
    /// Execute provided SQL script
    /// </summary>
    [HttpPost("execute")]
    public async Task<ActionResult<QueryResultTable>> ExecuteSqlScript([FromBody] InboundScriptDto inboundScriptDto)
    {
        return Ok(await _scriptService.ExecuteSqlScriptAsync(inboundScriptDto));
    }
}