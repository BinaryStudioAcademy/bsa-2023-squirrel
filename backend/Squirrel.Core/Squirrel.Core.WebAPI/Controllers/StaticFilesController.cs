using Microsoft.AspNetCore.Mvc;

namespace Squirrel.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaticFilesController : Controller
{
    private readonly IConfiguration _configuration;

    public StaticFilesController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("squirrel-installer"), DisableRequestSizeLimit]
    public async Task<IActionResult> DownloadSquirrelInstallerAsync()
    {
        var filePath = _configuration["ConsoleSetupFilePath"];

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        return File(
            await System.IO.File.ReadAllBytesAsync(filePath),
            "application/octet-stream",
            "SquirrelSetup.msi");
    }
}
