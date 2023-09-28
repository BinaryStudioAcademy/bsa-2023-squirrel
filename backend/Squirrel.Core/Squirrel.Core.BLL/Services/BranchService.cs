using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.DTO.Branch;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Squirrel.Core.DAL.Entities.JoinEntities;
using Squirrel.Shared.Exceptions;

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
        branch.IsActive = true;

        await EnsureUniquenessAsync(branch.Name, projectId);

        var createdBranch = (await _context.Branches.AddAsync(branch)).Entity;
        if (branchDto.ParentId is not null)
        {
            await InheritBranchInternalAsync(createdBranch, branchDto.ParentId ?? default);
        }
        await _context.SaveChangesAsync();

        return _mapper.Map<BranchDto>(createdBranch);
    }
    
    public async Task<(BranchCommit?, bool)> FindHeadBranchCommitAsync(Branch branch)
    {
        var currentBranch = branch;
        var isHeadOnAnotherBranch = false;
        while (currentBranch is not null)
        {
            var headBranchCommit = currentBranch.BranchCommits.FirstOrDefault(x => x.IsHead);
            if (headBranchCommit is not null)
            {
                return headBranchCommit.IsHead ? (headBranchCommit, isHeadOnAnotherBranch) : throw new Exception("Last commit should be head!");
            }
            currentBranch = await _context.Branches
                                          .Include(x => x.BranchCommits)
                                          .ThenInclude(x => x.Commit)
                                          .FirstOrDefaultAsync(x => x.Id == currentBranch.ParentBranchId);
            isHeadOnAnotherBranch = true;
        }

        return (null, isHeadOnAnotherBranch);
    }

    public async Task<int?> GetLastBranchCommitIdAsync(int branchId)
    {
        var branch = await _context.Branches
                                   .Include(x => x.BranchCommits)
                                   .FirstOrDefaultAsync(x => x.Id == branchId);
        if (branch is null)
        {
            throw new EntityNotFoundException();
        }

        return (await FindHeadBranchCommitAsync(branch)).Item1?.Id;
    }


    public BranchDto[] GetAllBranches(int projectId)
    {
        var branches = _context.Branches.Where(x => x.ProjectId == projectId);

        return _mapper.Map<BranchDto[]>(branches);
    }

    public async Task DeleteBranchAsync(int branchId)
    {
        var entity = await _context.Branches.FirstOrDefaultAsync(x => x.Id == branchId);
        if (entity is null)
        {
            throw new EntityNotFoundException();
        }
        entity.IsActive = false;
        _context.Branches.Update(entity);

        await _context.SaveChangesAsync();
    }

    public async Task<BranchDto> UpdateBranchAsync(int branchId, BranchUpdateDto branchUpdateDto)
    {
        var entity = await _context.Branches.FirstOrDefaultAsync(x => x.Id == branchId);
        if (entity is null)
        {
            throw new EntityNotFoundException();
        }
        await EnsureUniquenessAsync(branchUpdateDto.Name, entity.ProjectId);
        
        entity.Name = branchUpdateDto.Name;
        var updatedEntity = _context.Branches.Update(entity).Entity;

        await _context.SaveChangesAsync();
        return _mapper.Map<BranchDto>(updatedEntity);
    }

    private async Task InheritBranchInternalAsync(Branch branch, int parentId)
    {
        if (await _context.Branches.AnyAsync(x => x.Id == parentId && branch.ProjectId == x.ProjectId))
        {
            branch.ParentBranchId = parentId;
        }
    }

    private async Task EnsureUniquenessAsync(string branchName, int projectId)
    {
        if (await _context.Branches
            .AsNoTracking()
            .AnyAsync(branch =>
                branch.ProjectId == projectId &&
                string.Equals(branchName, branch.Name)))
        {
            throw new BranchAlreadyExistException();
        }
    }
}