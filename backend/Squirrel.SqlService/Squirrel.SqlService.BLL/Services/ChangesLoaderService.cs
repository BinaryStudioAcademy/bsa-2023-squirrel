using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.AzureBlobStorage.Models;
using Squirrel.SqlService.BLL.Interfaces;

namespace Squirrel.SqlService.BLL.Services;

public class ChangesLoaderService : IChangesLoaderService
{
    private const string UserDbChangesBlobContainerNameSection = "UserDbChangesBlobContainerName";
    private readonly IDbItemsRetrievalService _dbItemsRetrievalService;
    private readonly IBlobStorageService _blobStorageService;
    private readonly IConfiguration _configuration;

    public ChangesLoaderService(IDbItemsRetrievalService dbItemsRetrievalService, IBlobStorageService blobStorageService, IConfiguration configuration)
    {
        _dbItemsRetrievalService = dbItemsRetrievalService;
        _blobStorageService = blobStorageService;
        _configuration = configuration;
    }

    public async Task LoadChangesToBlobAsync(Guid changeId, Guid clientId)
    {
        // TODO get actual db structure
        var dbStructure = await _dbItemsRetrievalService.GetAllDbStructureAsync(clientId);
        var jsonString = JsonConvert.SerializeObject(dbStructure);
        var contentBytes = Encoding.UTF8.GetBytes(jsonString);
        var jsonMimeType = "application/json";

        var blob = new Blob
        {
            Id = changeId.ToString(),
            ContentType = jsonMimeType,
            Content = contentBytes
        };

        var containerName = _configuration[UserDbChangesBlobContainerNameSection];

        await _blobStorageService.UploadAsync(containerName, blob);
    }
}
