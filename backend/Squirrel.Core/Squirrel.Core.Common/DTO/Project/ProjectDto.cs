using Squirrel.Core.DAL.Enums;

namespace Squirrel.Core.Common.DTO.Project
{
    public sealed class ProjectDto
    {
        public string Name { get; set; } = null!;
        public string DefaultBranchName { get; set; } = null!;
        public DbEngine DbEngine { get; set; }
    }
}