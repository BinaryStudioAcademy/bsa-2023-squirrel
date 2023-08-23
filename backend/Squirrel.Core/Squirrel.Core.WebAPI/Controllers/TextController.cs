﻿using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.Text;

namespace Squirrel.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TextController : ControllerBase
{
    private readonly ITextService _textService;

    public TextController(ITextService textService)
    {
        _textService = textService;
    }

    /// <summary>
    /// Comapre two strings (texts) inline and Generates result
    /// </summary>
    /// <remarks>
    /// Sample reques:
    ///
    ///     POST /api/text/compare/inline
    ///     {
    ///        "oldText": "oldTextoldText oldTextoldText oldTextoldText ... etc",
    ///        "newText": "newTextnewText newTextnewText newTextnewText ... etc",
    ///        "ignoreWhitespace": false
    ///     }
    ///
    /// </remarks>
    [HttpPost("compare/inline")]
    [ProducesResponseType(typeof(InLineDiffResultDto), StatusCodes.Status200OK)]
    public ActionResult<InLineDiffResultDto> CompareInline([FromBody] TextPairRequestDto textPairDto)
    {
        return Ok(_textService.GetInlineDiffs(textPairDto));
    }

    /// <summary>
    /// Comapre two strings (texts) side by side and Generates result
    /// </summary>
    /// <remarks>
    /// Sample reques:
    ///
    ///     POST /api/text/compare/sidebyside
    ///     {
    ///        "oldText": "oldTextoldText oldTextoldText oldTextoldText ... etc",
    ///        "newText": "newTextnewText newTextnewText newTextnewText ... etc",
    ///        "ignoreWhitespace": false
    ///     }
    ///
    /// </remarks>
    [HttpPost("compare/sidebyside")]
    [ProducesResponseType(typeof(SideBySideDiffResultDto), StatusCodes.Status200OK)]
    public ActionResult<SideBySideDiffResultDto> CompareSideBySide([FromBody] TextPairRequestDto textPairDto)
    {
        return Ok(_textService.GetSideBySideDiffs(textPairDto));
    }
}