using Squirrel.Core.Common.DTO.Branch;

namespace Squirrel.Core.BLL.Interfaces;

public interface IBranchService
{
    Task<BranchDto> AddBranchAsync(BranchDto branchDto);
    Task<List<BranchDto>> GetBranchesByProjectAsync(int projectId);
    Task<BranchDto> GetBranchAsync(int branchId);
    Task DeleteBranchAsync(int branchId);
}