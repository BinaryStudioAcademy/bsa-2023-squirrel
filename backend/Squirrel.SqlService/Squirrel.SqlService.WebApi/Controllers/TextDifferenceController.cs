using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Shared.DTO.Text;
using Squirrel.SqlService.BLL.Interfaces;

namespace Squirrel.SqlService.WebApi.Controllers;

[Route("api/text-difference")]
[ApiController]
public class TextDifferenceController : ControllerBase
{
    private readonly ITextService _textService;

    public TextDifferenceController(ITextService textService)
    {
        _textService = textService;
    }

    /// <summary>
    /// Finds side-by-side differences in a pair of texts.
    /// </summary>
    [HttpPost("side-by-side")]
    public ActionResult<SideBySideDiffResultDto> FindSideBySideDifferences([FromBody] TextPairRequestDto textPairDto)
    {
        return Ok(_textService.GetSideBySideDiffs(textPairDto));
    }

    /// <summary>
    /// Finds inline differences in a pair of texts.
    /// </summary>
    [HttpPost("inline")]
    public ActionResult<InLineDiffResultDto> FindInLineDifferences([FromBody] TextPairRequestDto linePairDto)
    {
        return Ok(_textService.GetInlineDiffs(linePairDto));
    }
}
