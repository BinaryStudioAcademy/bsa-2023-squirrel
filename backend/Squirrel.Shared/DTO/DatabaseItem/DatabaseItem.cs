using Squirrel.Shared.Enums;

namespace Squirrel.Shared.DTO.DatabaseItem;

public sealed class DatabaseItem
{
    public string Name { get; set; } = null!;
    public DatabaseItemType Type { get; set; }
    public string Schema { get; set; } = null!;
}
