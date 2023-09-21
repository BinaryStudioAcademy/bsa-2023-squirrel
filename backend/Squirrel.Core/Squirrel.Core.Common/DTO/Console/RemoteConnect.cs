namespace Squirrel.Core.Common.DTO.Console;

public class RemoteConnect
{
    public ConnectionString DbConnection { get; set; } = null!;
    public string ClientId { get; set; } = null!;
}