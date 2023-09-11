﻿using Microsoft.Extensions.Configuration;
using Squirrel.Core.BLL.Interfaces;

namespace Squirrel.Core.BLL.Services;

public class StaticFilesService : IStaticFilesService
{
    private readonly IConfiguration _configuration;

    public StaticFilesService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Stream> GetSquirrelInstallerAsync()
    {
        var consoleSetupFilePath = _configuration["ConsoleSetupFilePath"];

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), consoleSetupFilePath);

        if (!System.IO.File.Exists(filePath))
        {
            throw new FileNotFoundException();
        }

        return await CopyFileToMemoryStreamAsync(filePath);
    }

    private static async Task<Stream> CopyFileToMemoryStreamAsync(string filePath)
    {
        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var memory = new MemoryStream();
        await fileStream.CopyToAsync(memory);
        memory.Position = 0;
        return memory;
    }
}
