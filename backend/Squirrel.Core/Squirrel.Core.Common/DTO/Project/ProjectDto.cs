﻿using Squirrel.Core.DAL.Enums;

namespace Squirrel.Core.Common.DTO.Project
{
    public sealed class ProjectDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DbEngine DbEngine { get; set; }
    }
}