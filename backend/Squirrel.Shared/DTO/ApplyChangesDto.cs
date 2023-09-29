using Squirrel.Core.DAL.Enums;

namespace Squirrel.Shared.DTO;

public class ApplyChangesDto
{
    public string? ClientId { get; set; }
    public DbEngine DbEngine { get; set; }
}