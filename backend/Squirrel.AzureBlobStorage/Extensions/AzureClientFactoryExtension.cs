using Azure.Core.Extensions;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Azure;

namespace Squirrel.AzureBlobStorage.Extensions
{
    public static class AzureClientFactoryExtension
    {
        public static IAzureClientBuilder<BlobServiceClient, BlobClientOptions> AddBlobServiceClient(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString, bool preferMsi)
        {
            if (preferMsi && Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out Uri? serviceUri))
            {
                return builder.AddBlobServiceClient(serviceUri);
            }
            return builder.AddBlobServiceClient(serviceUriOrConnectionString);
        }
    }
}
