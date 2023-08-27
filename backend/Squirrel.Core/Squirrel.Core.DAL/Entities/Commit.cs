using Squirrel.Core.DAL.Entities.Common.AuditEntity;

namespace Squirrel.Core.DAL.Entities;

public sealed class Commit : AuditEntity<int>
{
    public string Message { get; set; } = string.Empty;
}