using System.Reflection;
using AutoMapperBuilder.Extensions.DependencyInjection;
using Azure.Storage.Blobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Squirrel.AzureBlobStorage.Models;
using Squirrel.Core.BLL.MappingProfiles;

namespace Squirrel.Core.BLL.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapperBuilder(builder =>
        {
            builder.Profiles.Add(new UserProfile(
                services.BuildServiceProvider().GetRequiredService<BlobServiceClient>(),
                services.BuildServiceProvider().GetRequiredService<IOptions<BlobStorageOptions>>()));
        });
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}