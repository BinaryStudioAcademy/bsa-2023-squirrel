using Microsoft.AspNetCore.Mvc;
using Squirrel.Shared.DTO;
using Squirrel.SqlService.BLL.Interfaces;

namespace Squirrel.SqlService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplyChangesController : ControllerBase
    {
        private readonly IApplyChangesService _applyChangesService;
        public ApplyChangesController(IApplyChangesService applyChangesService)
        {
            _applyChangesService = applyChangesService;
        }
        [HttpPost("{commitId}")]
        public async Task<ActionResult> ApplyChangesAsync([FromBody] ApplyChangesDto applyChangesDto, int commitId)
        {
            await _applyChangesService.ApplyChanges(applyChangesDto, commitId);
            return Ok();
        }
    }
}
