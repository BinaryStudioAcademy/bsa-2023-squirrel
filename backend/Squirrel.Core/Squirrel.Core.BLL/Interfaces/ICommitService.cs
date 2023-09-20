using Squirrel.Core.Common.DTO.Commit;

namespace Squirrel.Core.BLL.Interfaces;
public interface ICommitService
{
    Task<CommitDto> CreateCommit(CreateCommitDto dto);
}
