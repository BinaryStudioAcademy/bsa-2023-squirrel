using Microsoft.Extensions.Configuration;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Shared.DTO;

namespace Squirrel.Core.BLL.Services;

public class ApplyChangesService: IApplyChangesService
{
    private readonly IHttpClientService _httpClientService;
    private readonly IConfiguration _configuration;

    public ApplyChangesService(IHttpClientService httpClientService, IConfiguration configuration)
    {
        _httpClientService = httpClientService;
        _configuration = configuration;
    }
    
    public async Task ApplyChanges(ApplyChangesDto applyChangesDto, int commitId)
    {
        await _httpClientService.SendAsync($"{_configuration["SqlServiceUrl"]}/api/ApplyChanges/{commitId}",
            applyChangesDto, HttpMethod.Post);
    }
}