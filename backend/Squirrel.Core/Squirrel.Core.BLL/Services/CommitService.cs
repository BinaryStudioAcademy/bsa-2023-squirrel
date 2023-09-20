using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.Common.DTO.Commit;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Squirrel.Core.DAL.Enums;
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

        await AddCommitToBranchAsync(dto.BranchId, commit);

        var items = await SaveFilesAsync(dto.ChangesGuid, commit.Id, dto.SelectedItems);

        var entity = await AddFilesToCommitAsync(commit, items);
        return _mapper.Map<CommitDto>(entity);
    }

    private async Task<SelectedItemsDto> SaveFilesAsync(string changesGuid, int commitId, ICollection<TreeNodeDto> nodes)
    {
        var selectedDto = new SelectedItemsDto
        {
            Guid = changesGuid,
            CommitId = commitId
        };

        MapTreeNodes(selectedDto, nodes);

        await _httpClientService.SendAsync
            ($"{_configuration["SqlServiceUrl"]}/api/CommitFiles/", selectedDto, HttpMethod.Post);

        return selectedDto;
    }

    private async Task AddCommitToBranchAsync(int branchId, Commit commit)
    {

        var branchEntity = _context.Branches.FirstOrDefault(x => x.Id == branchId);
        if (branchEntity == null)
        {
            throw new EntityNotFoundException(nameof(Branch), branchId);
        }
        branchEntity.Commits.Add(commit);
        _context.Branches.Update(branchEntity);

        await _context.SaveChangesAsync();
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

    private async Task<Commit> AddFilesToCommitAsync(Commit commit, SelectedItemsDto items)
    {
        foreach (var selected in items.StoredProcedures)
        {
            commit.CommitFiles.Add(
                new CommitFile 
                { 
                    BlobId = commit.Id.ToString(), FileName = selected, FileType = FileType.StoredProcedure 
                }
            );
        }
        foreach (var selected in items.Functions)
        {
            commit.CommitFiles.Add(
                new CommitFile
                {
                    BlobId = commit.Id.ToString(),
                    FileName = selected,
                    FileType = FileType.Function
                }
            );
        }
        foreach (var selected in items.Tables)
        {
            commit.CommitFiles.Add(
                new CommitFile
                {
                    BlobId = commit.Id.ToString(),
                    FileName = selected,
                    FileType = FileType.Table
                }
            );
        }
        foreach (var selected in items.Constraints)
        {
            commit.CommitFiles.Add(
            new CommitFile
                {
                    BlobId = commit.Id.ToString(),
                    FileName = selected,
                    FileType = FileType.Constraint
                }
            );
        }
        foreach (var selected in items.Types)
        {
            commit.CommitFiles.Add(
                new CommitFile
                {
                    BlobId = commit.Id.ToString(),
                    FileName = selected,
                    FileType = FileType.Type
                }
            );
        }
        commit.IsSaved = true;

        var result = _context.Commits.Update(commit).Entity;
        await _context.SaveChangesAsync();

        return result;
    }
}
