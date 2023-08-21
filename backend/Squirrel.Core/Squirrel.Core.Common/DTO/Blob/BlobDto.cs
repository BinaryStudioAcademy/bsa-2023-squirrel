namespace Squirrel.Core.Common.DTO.Blob
{
    public class BlobDto
    {
        public string Id { get; set; } = string.Empty;
        public string? AbsoluteUri { get; set; }
        public string ContentType { get; set; } = "octet-stream";
        public byte[]? Content { get; set; }
    }
}
