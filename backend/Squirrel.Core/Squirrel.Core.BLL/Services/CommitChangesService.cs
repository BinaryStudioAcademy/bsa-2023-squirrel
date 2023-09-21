using AutoMapper;
using Microsoft.Extensions.Configuration;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.DAL.Context;
using Squirrel.Shared.DTO.DatabaseItem;

namespace Squirrel.Core.BLL.Services;

public sealed class CommitChangesService: BaseService, ICommitChangesService
{
    private readonly IUserIdGetter _userIdGetter;
    private readonly IHttpClientService _httpClientService;
    private readonly IConfiguration _configuration;

    public CommitChangesService(SquirrelCoreContext context, IMapper mapper, IHttpClientService httpClientService, IUserIdGetter userIdGetter, IConfiguration configuration)
        : base(context, mapper)
    {
        _httpClientService = httpClientService;
        _userIdGetter = userIdGetter;
        _configuration = configuration;
    }
    
    public async Task<List<DatabaseItemContentCompare>> GetContentDiffsAsync(int commitId, Guid tempBlobId)
    {
        return await _httpClientService.GetAsync<List<DatabaseItemContentCompare>>(
            $"{_configuration["SqlServiceUrl"]}/api/ContentDifference/{commitId}/{tempBlobId}");
    }
}