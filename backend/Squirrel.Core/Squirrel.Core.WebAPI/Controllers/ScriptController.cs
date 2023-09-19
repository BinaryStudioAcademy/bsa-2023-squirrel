﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.Script;

namespace Squirrel.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
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
    public async Task<ActionResult<List<ScriptDto>>> GetAllScripts(int projectId)
    {
        return Ok(await _scriptService.GetAllScriptsAsync(projectId));
    }

    [HttpPost]
    public async Task<ActionResult<ScriptDto>> CreateScript(CreateScriptDto dto)
    {
        return Ok(await _scriptService.CreateScriptAsync(dto, _userIdGetter.GetCurrentUserId()));
    }

    [HttpPut]
    public async Task<ActionResult<ScriptDto>> UpdateScript(ScriptDto dto)
    {
        return Ok(await _scriptService.UpdateScriptAsync(dto, _userIdGetter.GetCurrentUserId()));
    }

    /// <summary>
    /// Find errors and format provided SQL script
    /// </summary>
    [HttpPut("format")]
    public async Task<ActionResult<ScriptContentDto>> GetFormattedSql([FromBody] InboundScriptDto inboundScriptDto)
    {
        return Ok(await _scriptService.GetFormattedSqlAsync(inboundScriptDto));
    }

    /// <summary>
    /// Execute provided SQL script
    /// </summary>
    [HttpPost("execute")]
    public ActionResult<ScriptResultDto> ExecuteFormattedSql([FromBody] InboundScriptDto inboundScriptDto)
    {
        // boilerplate for next PR

        return Ok();
    }
}