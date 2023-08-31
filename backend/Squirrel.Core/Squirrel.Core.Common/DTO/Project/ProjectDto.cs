using Squirrel.Core.DAL.Enums;

namespace Squirrel.Core.Common.DTO.Projects
{
    public sealed class ProjectDto
    {
        public string Name { get; set; }
        public DbEngine DbEngine { get; set; }
    }
}