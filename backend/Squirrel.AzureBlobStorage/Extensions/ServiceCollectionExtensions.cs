using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.AzureBlobStorage.Services;

namespace Squirrel.AzureBlobStorage.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAzureBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {            
        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddBlobServiceClient(configuration.GetConnectionString("BlobStorageConnectionString"), preferMsi: true);
        });
        services.AddTransient<IBlobStorageService, AzureBlobStorageService>();
    }
}