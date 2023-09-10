namespace Squirrel.Core.BLL.Interfaces;

public interface IStaticFilesService
{
    Task<MemoryStream> GetConsoleSetupAsync();
}
