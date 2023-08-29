namespace Squirrel.SqlService.WebApi.Options;

public class MongoDatabaseConnectionSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;
}
