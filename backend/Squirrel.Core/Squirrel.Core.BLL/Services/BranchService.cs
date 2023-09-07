using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.DTO.Branch;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Squirrel.Shared.Exceptions;
using System.Runtime.CompilerServices;

namespace Squirrel.Core.BLL.Services;

public sealed class BranchService : BaseService, IBranchService
{
    public BranchService(SquirrelCoreContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<BranchDto> AddBranchAsync(int projectId, BranchCreateDto branchDto)
    {
        var branch = _mapper.Map<Branch>(branchDto);
        branch.ProjectId = projectId;

        var createdBranch = (await _context.Branches.AddAsync(branch)).Entity;
        if (branchDto.ParentId != null)
        {
            await InheritBranchAsyncInternal(createdBranch, branchDto.ParentId ?? 0);
        }        
        await _context.SaveChangesAsync();

        return _mapper.Map<BranchDto>(createdBranch);
    }

    public BranchDto[] GetAllBranches(int projectId)
    {
        var branches = _context.Branches.Where(x => x.ProjectId == projectId);

        return _mapper.Map<BranchDto[]>(branches);
    }

    private async Task InheritBranchAsyncInternal(Branch branch, int parentId) 
    {
        var parent = (await _context.Branches.FirstOrDefaultAsync(x => x.Id == parentId)) ?? throw new EntityNotFoundException();

        branch.BranchCommits = parent.BranchCommits;
    }
}