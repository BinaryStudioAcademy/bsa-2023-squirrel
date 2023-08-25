using Squirrel.Core.Common.Enums;

namespace Squirrel.Core.Common.DTO.Projects
{
    public sealed class ProjectDto
    {
        public string Name { get; set; }
        public EngineEnum Engine { get; set; }
    }
}