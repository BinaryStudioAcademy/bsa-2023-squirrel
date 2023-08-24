using Squirrel.Core.DAL.Entities.Common;

namespace Squirrel.Core.DAL.Entities;

public sealed class Tag : Entity<int>
{
    public string Name { get; set; }
}