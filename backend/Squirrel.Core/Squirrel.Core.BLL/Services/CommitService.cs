﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.DTO.Commit;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Squirrel.Core.DAL.Enums;
using Squirrel.Shared.DTO.CommitFile;
using Squirrel.Shared.DTO.SelectedItems;
using Squirrel.Shared.Exceptions;
using System.Text;
using static Azure.Core.HttpHeader;

namespace Squirrel.Core.BLL.Services;
public class CommitService : BaseService, ICommitService
{
    private readonly IHttpClientService _httpClientService;
    private readonly IUserIdGetter _userIdGetter;
    public readonly IConfiguration _configuration;
    public CommitService(SquirrelCoreContext context, IMapper mapper, IHttpClientService httpClientService, IUserIdGetter userIdGetter, IConfiguration configuration) : base(context, mapper)
    {
        _httpClientService = httpClientService;
        _userIdGetter = userIdGetter;
        _configuration = configuration;
    }

    public async Task<CommitDto> CreateCommit(CreateCommitDto dto)
    {
        // Create commit
        var currentUserId = _userIdGetter.GetCurrentUserId();
        var user = _context.Users.FirstOrDefault(x => x.Id == currentUserId);
        if (user == null)
        {
            throw new EntityNotFoundException(nameof(User), currentUserId);
        }

        var commit = new Commit 
        { 
            Message = dto.Message, 
            PreScript = dto.PreScript, 
            PostScript = dto.PostScript, 
            Author = user 
        };

        var branchEntity = await GetBranchInternalAsync(dto.BranchId);

        await AddCommitToBranchAsync(branchEntity, commit);

        var items = await SaveFilesAsync(dto.ChangesGuid, commit.Id, dto.SelectedItems);
        // Update commit with saved files
        var entity = AddFilesToCommit(commit, items);
        // Add parent commit, if exist
        var head = branchEntity.BranchCommits.FirstOrDefault(x => x.IsHead);
        if (head != null)
        {
            var commitParent = new CommitParent
            {
                Commit = entity,
                ParentCommit = head.Commit,
            };
            _context.CommitParents.Add(commitParent);

            head.IsHead = false;
            _context.BranchCommits.Update(head);
        }
        // Update commit to be HEAD
        var branchCommit = await _context.BranchCommits
            .FirstOrDefaultAsync(x => x.BranchId == branchEntity.Id);
        if (branchCommit == null)
        {
            throw new EntityNotFoundException(nameof(branchEntity));
        }
        branchCommit.IsHead = true;
        _context.BranchCommits.Update(branchCommit);
        // Save changes
        await _context.SaveChangesAsync();
        return _mapper.Map<CommitDto>(entity);
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
        if (branchEntity == null)
        {
            throw new EntityNotFoundException(nameof(Branch), id);
        }

        return branchEntity;
    }

    private void MapTreeNodes(SelectedItemsDto selectedItems, ICollection<TreeNodeDto> nodes)
    {
        foreach (var section in nodes)
        {
            var children = section.Children.Where(x => x.Selected == true);
            foreach (var child in children)
            {
                if (section.Name == "Functions")
                {
                    selectedItems.Functions.Add(child.Name);
                }
                else if (section.Name == "Stored Procedures")
                {
                    selectedItems.StoredProcedures.Add(child.Name);
                }
                else if (section.Name == "Constraints")
                {
                    selectedItems.Constraints.Add(child.Name);
                }
                else if (section.Name == "Tables")
                {
                    selectedItems.Tables.Add(child.Name);
                }
                else if (section.Name == "Types")
                {
                    selectedItems.Types.Add(child.Name);
                }
                //...
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
