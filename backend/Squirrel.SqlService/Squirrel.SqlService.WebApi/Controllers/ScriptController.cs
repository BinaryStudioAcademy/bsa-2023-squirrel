using Microsoft.AspNetCore.Mvc;
using Squirrel.Shared.Exceptions;
using Squirrel.SqlService.BLL.Interfaces;
using Squirrel.SqlService.BLL.Models.DTO.Script;

namespace Squirrel.SqlService.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScriptController : ControllerBase
{
    private readonly ISqlFormatterService _sqlFormatterService;

    public ScriptController(ISqlFormatterService sqlFormatterService)
    {
        _sqlFormatterService = sqlFormatterService;
    }

    /// <summary>
    /// Find errors and format provided SQL script
    /// </summary>
    [HttpPut("format")]
    public ActionResult<ScriptContentDto> GetFormattedSql([FromBody] InboundScriptDto inboundScriptDto)
    {
        ValidateInput(inboundScriptDto);

        return Ok(_sqlFormatterService.GetFormattedSql(inboundScriptDto.DbEngine, inboundScriptDto.Content!));
    }

    /// <summary>
    /// Execute provided SQL script
    /// </summary>
    [HttpPost("execute")]
    public ActionResult<ScriptResultDto> ExecuteFormattedSql([FromBody] InboundScriptDto inboundScriptDto)
    {
        ValidateInput(inboundScriptDto);

        // boilerplate for next PR

        var scriptToExecute = _sqlFormatterService.GetFormattedSql(inboundScriptDto.DbEngine, inboundScriptDto.Content!);

        return Ok();
    }


    private void ValidateInput(InboundScriptDto inboundScriptDto)
    {
        if (inboundScriptDto == null || string.IsNullOrEmpty(inboundScriptDto.Content))
        {
            throw new SqlSyntaxException("InputSql cannot be Null or Empty");
        }
    }
}