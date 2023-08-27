using Squirrel.Core.Common.Enums;
using Squirrel.Core.DAL.Entities.Common.AuditEntity;

namespace Squirrel.Core.DAL.Entities;

public sealed class Project : AuditEntity<int>
{
    public string Name { get; set; }
    public DbEngine DbEngine { get; set; }
}