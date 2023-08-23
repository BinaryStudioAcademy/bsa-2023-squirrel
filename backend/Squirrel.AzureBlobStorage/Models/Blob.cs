namespace Squirrel.AzureBlobStorage.Models;

public class Blob
{
    public string Id { get; set; } = string.Empty;
    public string ContentType { get; set; } = "octet-stream";
    public byte[]? Content { get; set; }
}