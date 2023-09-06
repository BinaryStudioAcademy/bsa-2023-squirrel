using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services;
using Squirrel.Core.Common.DTO.Branch;
using Squirrel.Core.Common.DTO.Project;

namespace Squirrel.Core.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        [HttpGet("byProject/{projectId}")]
        public async Task<ActionResult<List<BranchDto>>> GetBranchesByProject(int projectId)
        {
            return Ok(await _branchService.GetBranchesByProjectAsync(projectId));
        }

        [HttpPost]
        public async Task<ActionResult<BranchDto>> AddBranch([FromBody] BranchDto branchDto)
        {
            return Ok(await _branchService.AddBranchAsync(branchDto));
        }

        [HttpGet("{branchId}")]
        public async Task<ActionResult<BranchDto>> GetBranch(int branchId)
        {
            return Ok(await _branchService.GetBranchAsync(branchId));
        }

        [HttpDelete("{branchId}")]
        public async Task<IActionResult> DeleteBranch(int branchId)
        {
            await _branchService.DeleteBranchAsync(branchId);
            return NoContent();
        }
    }
}
