using Squirrel.Shared.DTO;

namespace Squirrel.SqlService.BLL.Interfaces
{
    public interface IApplyChangesService
    {
        Task ApplyChanges(ApplyChangesDto applyChangesDto, int commitId);
    }
}
