using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Squirrel.Core.WebAPI.Extensions;

public static class ApplicationBuilderExtension
{
    public static void UseAvatarContainer(this IApplicationBuilder app)
    {
        var blobServiceClient = app.ApplicationServices.GetService<BlobServiceClient>();
        var containerClient = blobServiceClient?.GetBlobContainerClient("user-avatars");
        containerClient?.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
    }
}