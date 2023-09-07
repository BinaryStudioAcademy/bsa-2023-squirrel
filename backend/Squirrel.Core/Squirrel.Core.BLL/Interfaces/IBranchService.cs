using Squirrel.Core.Common.DTO.Branch;

namespace Squirrel.Core.BLL.Interfaces;

public interface IBranchService
{
    Task<BranchDto> AddBranchInternalAsync(BranchDto branchDto);
    Task<BranchDto> AddBranchAsync(BranchCreateDto branchDto);
    BranchDto[] GetAllBranches(int projectId);
}