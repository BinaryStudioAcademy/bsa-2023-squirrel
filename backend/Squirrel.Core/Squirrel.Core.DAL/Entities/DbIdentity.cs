using Squirrel.Core.DAL.Entities.Common;
using Squirrel.Core.DAL.Enums;

namespace Squirrel.Core.DAL.Entities;

public sealed class DbIdentity : Entity<int>
{
    public string DbName { get; set; } = string.Empty;
    public DbEngine DbEngine { get; set; }
    public Guid Guid { get; set; }

    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}