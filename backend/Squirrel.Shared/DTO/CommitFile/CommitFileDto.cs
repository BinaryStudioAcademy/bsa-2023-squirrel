using Squirrel.Shared.Enums;

namespace Squirrel.Shared.DTO.CommitFile;

public class CommitFileDto
{
    public string FileName { get; set; } = string.Empty;
    public DatabaseItemType FileType { get; set; }
    public string BlobId { get; set; } = string.Empty;
}
