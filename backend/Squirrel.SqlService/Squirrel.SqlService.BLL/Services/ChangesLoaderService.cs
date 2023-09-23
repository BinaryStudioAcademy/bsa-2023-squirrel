using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.AzureBlobStorage.Models;
using Squirrel.SqlService.BLL.Interfaces;
using System.Text;

namespace Squirrel.SqlService.BLL.Services;

public class ChangesLoaderService : IChangesLoaderService
{
    // mocked data for now
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

        // Serialize the dbStructure to a JSON string
        var jsonString = JsonConvert.SerializeObject(dbStructure);

        // Convert the JSON string to a byte array using UTF-8 encoding
        byte[] contentBytes = Encoding.UTF8.GetBytes(jsonString);

        var blob = new Blob
        {
            Id = changeId.ToString(),
            ContentType = "application/json",
            Content = contentBytes
        };

        var containerName = _configuration["UserDbChangesBlobContainerName"];

        await _blobStorageService.UploadAsync(containerName, blob);
    }
}
