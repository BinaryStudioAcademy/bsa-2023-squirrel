using Squirrel.Core.DAL.Enums;

namespace Squirrel.Core.Common.DTO.Project
{
    public sealed class ProjectDto
    {
        public string Name { get; set; } = string.Empty;
        public DbEngine DbEngine { get; set; }
    }
}