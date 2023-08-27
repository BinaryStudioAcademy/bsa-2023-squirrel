using Squirrel.Core.DAL.Entities.Common;
using Squirrel.Core.DAL.Enums;

namespace Squirrel.Core.DAL.Entities;

public sealed class CommitFile : Entity<int>
{
    public string FileName { get; set; } = string.Empty;
    public FileType FileType { get; set; }
    public string BlobId { get; set; } = string.Empty;
    
    public int CommitId { get; set; }
}
