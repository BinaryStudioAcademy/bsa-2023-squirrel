using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO.Branch;

namespace Squirrel.Core.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BranchController : ControllerBase
{
    private readonly IBranchService _branchService;

    public BranchController(IBranchService branchService)
    {
        _branchService = branchService;
    }

    [HttpGet("{projectId}")]
    public ActionResult<List<BranchDto>> GetAllBranches(int projectId)
    {
        return Ok(_branchService.GetAllBranches(projectId));
    }

    [HttpPost("{projectId}")]
    public async Task<ActionResult<BranchDto>> AddBranch(int projectId, [FromBody] BranchCreateDto dto) 
    { 
        return Ok(await _branchService.AddBranchAsync(projectId, dto));
    } 


}
