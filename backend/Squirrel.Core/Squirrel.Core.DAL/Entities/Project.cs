using Squirrel.Core.DAL.Entities.Common.AuditEntity;

namespace Squirrel.Core.DAL.Entities;

public sealed class Project : AuditEntity<int>
{
    public string Name { get; set; }
    public int Engine { get; set; }
}