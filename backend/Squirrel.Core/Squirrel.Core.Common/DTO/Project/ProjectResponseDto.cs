using Squirrel.Core.Common.DTO.Tag;
using Squirrel.Core.DAL.Enums;

namespace Squirrel.Core.Common.DTO.Project;

public sealed class ProjectResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DbEngine DbEngine { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<TagDto>? Tags { get; set; }
}