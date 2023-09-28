using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.DTO.Commit;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Squirrel.Core.DAL.Entities.JoinEntities;
using Squirrel.Shared.DTO.CommitFile;
using Squirrel.Shared.DTO.SelectedItems;
using Squirrel.Shared.Enums;
using Squirrel.Shared.Exceptions;

namespace Squirrel.Core.BLL.Services;

public class CommitService : BaseService, ICommitService
{
    private readonly IHttpClientService _httpClientService;
    private readonly IUserIdGetter _userIdGetter;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly IBranchService _branchService;

    public CommitService(
        SquirrelCoreContext context,
        IMapper mapper,
        IHttpClientService httpClientService,
        IUserIdGetter userIdGetter,
        IConfiguration configuration,
        IUserService userService,
        IBranchService branchService) : base(context, mapper)
    {
        _httpClientService = httpClientService;
        _userIdGetter = userIdGetter;
        _configuration = configuration;
        _userService = userService;
        _branchService = branchService;
    }

    public async Task<CommitDto> CreateCommitAsync(CreateCommitDto dto)
    {
        var currentUserId = _userIdGetter.GetCurrentUserId();
        var user = await _userService.GetUserByIdInternalAsync(currentUserId);
        var commit = _mapper.Map<Commit>(dto);
        commit!.Author = user;

        var branchEntity = await GetBranchInternalAsync(dto.BranchId);
        await AddCommitToBranchAsync(branchEntity, commit);

        var savedCommitFiles = await SaveFilesAsync(dto.ChangesGuid, commit.Id, dto.SelectedItems);
        var updatedCommit = AddFilesToCommit(commit, savedCommitFiles);
        
        var (headBranchCommit, isHeadOnAnotherBranch) = await _branchService.FindHeadBranchCommitAsync(branchEntity);
        if (headBranchCommit is not null)
        {
            var commitParent = new CommitParent
            {
                Commit = updatedCommit,
                ParentCommit = headBranchCommit.Commit,
            };
            await _context.CommitParents.AddAsync(commitParent);
        
            headBranchCommit.IsHead = isHeadOnAnotherBranch;
            _context.BranchCommits.Update(headBranchCommit);
        }
        
        var branchCommit = await _context.BranchCommits
            .FirstOrDefaultAsync(x =>
                x.CommitId == updatedCommit.Id &&
                x.BranchId == branchEntity.Id);
        if (branchCommit is null)
        {
            throw new EntityNotFoundException(nameof(branchCommit));
        }
        branchCommit.IsHead = true;
        _context.BranchCommits.Update(branchCommit);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<CommitDto>(updatedCommit)!;
    }

    private async Task<ICollection<CommitFileDto>> SaveFilesAsync(string changesGuid, int commitId, ICollection<TreeNodeDto> nodes)
    {
        var selectedDto = new SelectedItemsDto
        {
            Guid = changesGuid,
            CommitId = commitId
        };

        MapTreeNodes(selectedDto, nodes);

        return await _httpClientService.SendAsync<SelectedItemsDto, ICollection<CommitFileDto>>
            ($"{_configuration["SqlServiceUrl"]}/api/CommitFiles/", selectedDto, HttpMethod.Post);
    }

    private async Task AddCommitToBranchAsync(Branch branchEntity, Commit commit)
    {
        branchEntity.Commits.Add(commit);
        _context.Branches.Update(branchEntity);

        await _context.SaveChangesAsync();
    }

    private async Task<Branch> GetBranchInternalAsync(int id)
    {
        var branchEntity = await _context.Branches
            .AsSplitQuery()
            .Include(x => x.BranchCommits)
                .ThenInclude(x => x.Commit)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (branchEntity is null)
        {
            throw new EntityNotFoundException(nameof(Branch), id);
        }

        return branchEntity;
    }

    private void MapTreeNodes(SelectedItemsDto selectedItems, ICollection<TreeNodeDto> nodes)
    {
        foreach (var section in nodes)
        {
            var children = section.Children.Where(x => x.Selected);
            switch (section.Name)
            {
                case ItemCategory.Function:
                    selectedItems.Functions.AddRange(children.Select(child => child.Name));
                    break;
                case ItemCategory.StoredProcedure:
                    selectedItems.StoredProcedures.AddRange(children.Select(child => child.Name));
                    break;
                case ItemCategory.Constraint:
                    selectedItems.Constraints.AddRange(children.Select(child => child.Name));
                    break;
                case ItemCategory.Table:
                    selectedItems.Tables.AddRange(children.Select(child => child.Name));
                    break;
                case ItemCategory.Type:
                    selectedItems.Types.AddRange(children.Select(child => child.Name));
                    break;
                case ItemCategory.View:
                    selectedItems.Views.AddRange(children.Select(child => child.Name));
                    break;
            }
        }
    }

    private Commit AddFilesToCommit(Commit commit, ICollection<CommitFileDto> files)
    {
        var commitFileEntities = _mapper.Map<ICollection<CommitFile>>(files);
        foreach (var item in commitFileEntities)
        {
            commit.CommitFiles.Add(item);
        }
        commit.IsSaved = true;

        return _context.Commits.Update(commit).Entity;
    }
}
