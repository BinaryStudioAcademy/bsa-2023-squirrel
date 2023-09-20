namespace Squirrel.Core.BLL.Interfaces;

public interface ICommitFileService
{
    Task<List<string>> GetBlobIdsByCommitId(int commitId);
}