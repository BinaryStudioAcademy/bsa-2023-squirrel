using Microsoft.Extensions.Azure;
using Squirrel.AzureBlobStorage.WebApi.Interfaces;
using Squirrel.AzureBlobStorage.WebApi.Services;

namespace Squirrel.AzureBlobStorage.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterCustomServices(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddTransient<IBlobStorageService, AzureBlobStorageService>();
        }

        public static void AddAzureBlobStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAzureClients(clientBuilder =>
            {
                clientBuilder.AddBlobServiceClient(configuration.GetConnectionString("BlobStorageConnectionString"), preferMsi: true);
            });
        }
    }
}
