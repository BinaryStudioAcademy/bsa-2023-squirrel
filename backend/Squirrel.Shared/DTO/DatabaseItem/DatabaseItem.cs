using Squirrel.Shared.Enums;

namespace Squirrel.Shared.DTO.DatabaseItem;

public sealed class DatabaseItem
{
    public string Name { get; set; }
    public DatabaseItemType Type { get; set; }

    public DatabaseItem(string name, DatabaseItemType type)
    {
        Name = name;
        Type = type;
    }
}
