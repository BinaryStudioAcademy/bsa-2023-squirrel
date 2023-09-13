using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.AzureBlobStorage.Models;
using Squirrel.Core.DAL.Entities;
using Squirrel.Shared.DTO.DatabaseItem;
using System.Text;

namespace Squirrel.Core.BLL.Interfaces;

public class DBStructureSaverService : IDBStructureSaverService
{
    private readonly IHttpClientService _httpClientService;
    private readonly IConfiguration _configuration;
    private readonly IBlobStorageService _blobStorageService;

    public DBStructureSaverService(IHttpClientService httpClientService, IConfiguration configuration, IBlobStorageService blobStorageService)
    {
        _httpClientService = httpClientService;
        _configuration = configuration;
        _blobStorageService = blobStorageService;
    }

    public async Task SaveDBStructureToAzureBlob(ChangeRecord changeRecord)
    {
        // TODO get actual db structure
        var dbStructure = await _httpClientService.GetAsync<List<DatabaseItem>>
            ($"{_configuration["SqlServiceUrl"]}/api/DatabaseItems");

        // Serialize the dbStructure to a JSON string
        var jsonString = JsonConvert.SerializeObject(dbStructure);

        // Convert the JSON string to a byte array using UTF-8 encoding
        byte[] contentBytes = Encoding.UTF8.GetBytes(jsonString);

        var blob = new Blob
        {
            Id = changeRecord.UniqueChangeId.ToString(),
            ContentType = "application/json",
            Content = contentBytes
        };

        var containerName = _configuration["UserDbChangesBlobContainerName"];

        await _blobStorageService.UploadAsync(containerName, blob);
    }
}