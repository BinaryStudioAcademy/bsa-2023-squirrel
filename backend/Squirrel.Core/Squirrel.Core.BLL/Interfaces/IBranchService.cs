using Squirrel.Core.Common.DTO.Branch;

namespace Squirrel.Core.BLL.Interfaces;

public interface IBranchService
{
    Task<BranchDto> AddBranchAsync(int projectId, BranchCreateDto branchDto);
    BranchDto[] GetAllBranches(int projectId);
    Task<BranchDto> UpdateBranch(int branchId, BranchUpdateDto branchUpdateDto);
    Task DeleteBranch(int branchId);
}