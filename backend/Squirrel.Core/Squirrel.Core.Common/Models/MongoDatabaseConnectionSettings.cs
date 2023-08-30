namespace Squirrel.Core.Common.Models;

public class MongoDatabaseConnectionSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;
}
