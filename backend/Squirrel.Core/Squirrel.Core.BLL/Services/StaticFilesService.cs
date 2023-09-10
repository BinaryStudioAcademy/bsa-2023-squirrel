using Microsoft.Extensions.Configuration;
using Squirrel.Core.BLL.Interfaces;

namespace Squirrel.Core.BLL.Services;

public class StaticFilesService : IStaticFilesService
{
    private readonly IConfiguration _configuration;

    public StaticFilesService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<MemoryStream> GetConsoleSetupAsync()
    {
        var consoleSetupFilePath = _configuration["ConsoleSetupFilePath"];

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), consoleSetupFilePath);

        if (!System.IO.File.Exists(filePath))
        {
            throw new FileNotFoundException();
        }

        var memory = new MemoryStream();
        await using (var stream = new FileStream(filePath, FileMode.Open))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;

        return memory;
    }
}
