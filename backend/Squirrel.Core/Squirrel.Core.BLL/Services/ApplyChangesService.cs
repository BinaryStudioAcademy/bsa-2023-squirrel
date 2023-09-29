using Microsoft.Extensions.Configuration;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Shared.DTO;

namespace Squirrel.Core.BLL.Services;

public class ApplyChangesService: IApplyChangesService
{
    private readonly IHttpClientService _httpClientService;
    private readonly IConfiguration _configuration;
    private readonly IBranchService _branchService;

    public ApplyChangesService(IHttpClientService httpClientService, IConfiguration configuration, IBranchService branchService)
    {
        _httpClientService = httpClientService;
        _configuration = configuration;
        _branchService = branchService;
    }
    
    public async Task ApplyChanges(ApplyChangesDto applyChangesDto, int branchId)
    {
        var lustCommitId = await _branchService.GetLastBranchCommitIdAsync(branchId);
        await _httpClientService.SendAsync($"{_configuration["SqlServiceUrl"]}/api/ApplyChanges/{lustCommitId}",
            applyChangesDto, HttpMethod.Post);
    }
}