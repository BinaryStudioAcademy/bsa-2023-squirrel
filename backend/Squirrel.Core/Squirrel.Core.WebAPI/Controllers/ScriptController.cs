using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
}