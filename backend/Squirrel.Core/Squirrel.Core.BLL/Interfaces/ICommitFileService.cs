namespace Squirrel.Core.BLL.Interfaces;

public interface ICommitFileService
{
    Task<List<string>> GetBlobIdsByCommitIdAsync(int commitId);
}