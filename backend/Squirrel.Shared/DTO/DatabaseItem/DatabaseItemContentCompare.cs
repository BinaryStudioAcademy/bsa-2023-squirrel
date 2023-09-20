using DiffPlex.DiffBuilder;
using Squirrel.Shared.DTO.Text;
using Squirrel.Shared.Enums;

namespace Squirrel.Shared.DTO.DatabaseItem;

public sealed class DatabaseItemContentCompare
{
    public string SchemaName { get; set; } = null!;
    public string ItemName { get; set; } = null!;
    
    public DatabaseItemType ItemType { get; set; }
    
    public SideBySideDiffResultDto? SideBySideDiff { get; set; }
    public InLineDiffResultDto? InLineDiff { get; set; }
}