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
    private readonly IUserIdGetter _userIdGetter;
    private readonly IUserService _userService;
    public BranchService(SquirrelCoreContext context, IMapper mapper, IUserIdGetter userIdGetter, IUserService userService) : base(context, mapper)
    {
        _userIdGetter = userIdGetter;
        _userService = userService;
    }

    public async Task<BranchDto> AddBranchAsync(int projectId, BranchCreateDto branchDto)
    {
        var userId = _userIdGetter.GetCurrentUserId();
        var user = await _userService.GetUserByIdInternalAsync(userId) ?? throw new EntityNotFoundException(nameof(User), userId);

        var branch = _mapper.Map<Branch>(branchDto);
        branch.ProjectId = projectId;
        branch.CreatedBy = userId;
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
        var commits = await GetCommitsFromBranchInternalAsync(branchId, default);
        return commits.FirstOrDefault()?.Id ?? default;
    }

    public async Task<BranchDto> MergeBranchAsync(int sourceId, int destId)
    {
        var dest = await GetFullBranchEntityAsync(destId) ?? throw new EntityNotFoundException(nameof(Branch), destId);

        BranchCommit? lastCommit = default;
        foreach (var commit in await GetCommitsFromBranchInternalAsync(sourceId, destId))
        {
            var branchCommit = new BranchCommit
            {
                CommitId = commit.Id,
                BranchId = dest.Id,
                IsMerged = true
            };
            dest.BranchCommits.Add(branchCommit);
            lastCommit = branchCommit;
        }

        if (lastCommit is not null)
        {
            lastCommit.IsHead = true;
            var previousHead = await FindHeadBranchCommitAsync(dest);
            if (previousHead.Item2 && previousHead.Item1 is not null)
            {
                previousHead.Item1.IsHead = false;
                _context.BranchCommits.Update(previousHead.Item1);
            }
        }

        var entity = _context.Branches.Update(dest).Entity;
        await _context.SaveChangesAsync();

        return _mapper.Map<BranchDto>(entity);
    }

    public async Task<ICollection<Commit>> GetCommitsFromBranchInternalAsync(int branchId, int destinationId)
    {
        var source = await GetFullBranchEntityAsync(branchId) ?? throw new EntityNotFoundException(nameof(Branch), branchId);
        var createdAt = source.CreatedAt;
        var isOriginal = true;
        var commits = new List<Commit>();

        while (source is not null && source.Id != destinationId)
        {
            foreach (var commit in source.Commits
                .Where(x => isOriginal ? true : x.CreatedAt <= createdAt)
                .OrderByDescending(x => x.CreatedAt))
            {
                commits.Add(commit);
            }

            isOriginal = false;
            source = await GetFullBranchEntityAsync(source.ParentBranchId ?? default);
        }

        return commits;
    }
    
    public BranchDto[] GetAllBranches(int projectId)
    {
        var branches = _context.Branches.Where(x => x.ProjectId == projectId);

        return _mapper.Map<BranchDto[]>(branches);
    }

    public async Task<ICollection<BranchDetailsDto>> GetAllBranchDetailsAsync(int projectId, int selectedBranch)
    {
        var entities = _context.Branches
            .Include(x => x.Author)
            .Where(x => x.ProjectId == projectId);
        var branches = _mapper.Map<List<BranchDetailsDto>>(entities);

        var selectedBranchCommits = await GetCommitsFromBranchInternalAsync(selectedBranch, 0);
        foreach (var branch in branches)
        {
            var branchCommits = await GetCommitsFromBranchInternalAsync(branch.Id, 0);
            var aheadCommits = branchCommits.Where(x => !selectedBranchCommits.Any(y => y.Id == x.Id));

            var behindCommits = selectedBranchCommits.Where(x => !branchCommits.Any(y => y.Id == x.Id));

            branch.Ahead = aheadCommits.Count();
            branch.Behind = behindCommits.Count();
        }

        return branches;
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

    private async Task<Branch?> GetFullBranchEntityAsync(int branchId)
    {
        return await _context.Branches
            .AsSplitQuery()
            .Include(x => x.Commits)
            .Include(x => x.ParentBranch)
            .Include(x => x.BranchCommits)
                .ThenInclude(x => x.Commit)
            .FirstOrDefaultAsync(x => x.Id == branchId);
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