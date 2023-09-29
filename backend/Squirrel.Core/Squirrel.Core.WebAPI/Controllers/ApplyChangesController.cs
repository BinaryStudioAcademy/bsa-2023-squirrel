using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Shared.DTO;

namespace Squirrel.Core.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ApplyChangesController: ControllerBase
{
    private readonly IApplyChangesService _applyChangesService;

    public ApplyChangesController(IApplyChangesService applyChangesService)
    {
        _applyChangesService = applyChangesService;
    }
    
    [HttpPost("{branchId}")]
    public async Task<ActionResult> ApplyChangesAsync([FromBody] ApplyChangesDto applyChangesDto, int branchId)
    {
        await _applyChangesService.ApplyChanges(applyChangesDto, branchId);
        return Ok();
    }
}