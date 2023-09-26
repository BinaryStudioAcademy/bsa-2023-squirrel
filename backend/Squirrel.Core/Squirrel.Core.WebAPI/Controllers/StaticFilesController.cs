using Microsoft.AspNetCore.Mvc;
using OperatingSystem = Squirrel.Core.Common.Models.OperatingSystem;

namespace Squirrel.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaticFilesController : Controller
{
    private const string ConsoleSetupFilePathSection = "ConsoleSetupFilePath";
    private const string OctetStreamMimeTypeName = "application/octet-stream";
    private const string ConsoleSetupFileName = "SquirrelSetup.exe";
    private readonly IConfiguration _configuration;

    public StaticFilesController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [HttpGet("squirrel-installer/{operatingSystem}"), DisableRequestSizeLimit]
    public async Task<IActionResult> DownloadSquirrelInstallerAsync(OperatingSystem operatingSystem)
    {
        // Path will be updated using 'operatingSystem' after task 152
        var filePath = _configuration[ConsoleSetupFilePathSection];

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        // FileDownloadName will be updated using 'operatingSystem' after task 152
        return File(
            await System.IO.File.ReadAllBytesAsync(filePath),
            OctetStreamMimeTypeName,
            ConsoleSetupFileName);
    }
}
