using Squirrel.Core.Common.DTO.Branch;
using Squirrel.Core.DAL.Entities;
using Squirrel.Core.DAL.Entities.JoinEntities;

namespace Squirrel.Core.BLL.Interfaces;

public interface IBranchService
{
    Task<BranchDto> AddBranchAsync(int projectId, BranchCreateDto branchDto);
    BranchDto[] GetAllBranches(int projectId);
    Task<int?> GetLastBranchCommitIdAsync(int branchId);
    Task<(BranchCommit?, bool)> FindHeadBranchCommitAsync(Branch branch);
    Task<BranchDto> UpdateBranchAsync(int branchId, BranchUpdateDto branchUpdateDto);
    Task DeleteBranchAsync(int branchId);
}