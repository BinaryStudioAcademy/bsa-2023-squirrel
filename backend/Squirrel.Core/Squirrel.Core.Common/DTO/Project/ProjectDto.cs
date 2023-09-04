using Squirrel.Core.DAL.Enums;

namespace Squirrel.Core.Common.DTO.Project
{
    public sealed class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DbEngine DbEngine { get; set; }
    }
}