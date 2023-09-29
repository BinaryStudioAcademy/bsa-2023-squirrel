using Squirrel.Shared.DTO;

namespace Squirrel.Core.BLL.Interfaces;

public interface IApplyChangesService
{
    Task ApplyChanges(ApplyChangesDto applyChangesDto, int commitId);
}