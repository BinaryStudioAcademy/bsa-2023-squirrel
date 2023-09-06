using AutoMapper;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.DTO.Branch;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.Services;

public sealed class BranchService : BaseService, IBranchService
{
    public BranchService(SquirrelCoreContext context, IMapper mapper) : base(context, mapper)
    {
    }
    
    public async Task<BranchDto> AddBranchAsync(BranchDto branchDto)
    {
        var branch = _mapper.Map<Branch>(branchDto);
        var createdBranch = (await _context.Branches.AddAsync(branch)).Entity;
        await _context.SaveChangesAsync();

        return _mapper.Map<BranchDto>(createdBranch);
    }
}