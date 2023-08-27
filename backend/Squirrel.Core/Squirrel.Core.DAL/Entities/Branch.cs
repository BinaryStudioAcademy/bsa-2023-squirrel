using Squirrel.Core.DAL.Entities.Common;

namespace Squirrel.Core.DAL.Entities;

public sealed class Branch : Entity<int>
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    
    public int ProjectId { get; set; }
}