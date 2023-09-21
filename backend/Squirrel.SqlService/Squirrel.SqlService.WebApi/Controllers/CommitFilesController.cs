using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Shared.DTO.CommitFile;
using Squirrel.Shared.DTO.SelectedItems;
using Squirrel.SqlService.BLL.Interfaces;
using Squirrel.SqlService.BLL.Models.DTO;

namespace Squirrel.SqlService.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CommitFilesController : ControllerBase
{
    private readonly ICommitFilesService _commitFilesService;

    public CommitFilesController(ICommitFilesService commitFilesService)
    {
        _commitFilesService = commitFilesService;
    }

    [HttpPost]
    public async Task<ActionResult<ICollection<CommitFileDto>>> Save(SelectedItemsDto dto)
    {
        return Ok(await _commitFilesService.SaveSelectedFiles(dto));
    }
    // TEST ENDPOINT
    [HttpGet]
    public ActionResult<DbStructureDto> GetStructure()
    {
        return Ok(_commitFilesService.GetTestStructure());
    }
}
