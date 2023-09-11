using Microsoft.AspNetCore.Mvc;
using Squirrel.Core.BLL.Interfaces;

namespace Squirrel.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaticFilesController : Controller
{
    private readonly IStaticFilesService _staticFilesService;

    public StaticFilesController(IStaticFilesService staticFilesService)
    {
        _staticFilesService = staticFilesService;
    }

    [HttpGet("squirrel-installer"), DisableRequestSizeLimit]
    public async Task<IActionResult> DownloadSquirrelInstaller()
    {
        var memory = await _staticFilesService.GetSquirrelInstallerAsync();

        return File(memory, "application/octet-stream", "SquirrelSetup.msi");
    }
}
