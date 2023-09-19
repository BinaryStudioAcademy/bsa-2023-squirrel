using Squirrel.Shared.Enums;

namespace Squirrel.Shared.DTO.DatabaseItem;

public sealed class DatabaseItem
{
    public string Name { get; set; }
    public DatabaseItemType Type { get; set; }
    public string Schema { get; set; }

    public DatabaseItem(string name, DatabaseItemType type, string schema)
    {
        Name = name;
        Type = type;
        Schema = schema;
    }
}
