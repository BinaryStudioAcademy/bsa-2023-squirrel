﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.DTO.Branch;
using Squirrel.Core.Common.DTO.Project;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Squirrel.Shared.Exceptions;

namespace Squirrel.Core.BLL.Services;

public sealed class BranchService : BaseService, IBranchService
{
    public BranchService(SquirrelCoreContext context, IMapper mapper) : base(context, mapper)
    {
    }
    
    public async Task<BranchDto> AddBranchAsync(BranchDto branchDto)
    {
        var branch = _mapper.Map<Branch>(branchDto);

        if(branchDto.ParentBranchId is not null)
        {
            var parentBranch = await GetBranchAsync((int)branchDto.ParentBranchId);

            branch.Commits = parentBranch.Commits;
            //TO DO
        }

        var createdBranch = (await _context.Branches.AddAsync(branch)).Entity;
        await _context.SaveChangesAsync();

        return _mapper.Map<BranchDto>(createdBranch);
    }

    public async Task DeleteBranchAsync(int branchId)
    {
        var branch = await _context.Branches.FindAsync(branchId);
        if (branch is null)
        {
            throw new EntityNotFoundException();
        }

        _context.Branches.Remove(branch);
        await _context.SaveChangesAsync();
    }

    public async Task<BranchDto> GetBranchAsync(int branchId)
    {
        var branch = await _context.Branches.FindAsync(branchId);
        if (branch is null)
        {
            throw new EntityNotFoundException();
        }

        return _mapper.Map<BranchDto>(branch)!;
    }

    public async Task<List<BranchDto>> GetBranchesByProjectAsync(int projectId)
    {
        var branches = await _context.Branches.Where(b => b.ProjectId == projectId).ToListAsync();

        return _mapper.Map<List<BranchDto>>(branches);
    }
}