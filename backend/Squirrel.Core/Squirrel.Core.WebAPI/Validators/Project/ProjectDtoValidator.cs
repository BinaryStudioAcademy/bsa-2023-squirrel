using FluentValidation;
using Squirrel.Core.Common.DTO.Projects;

namespace Squirrel.Core.WebAPI.Validators.Project;

public class ProjectDtoValidator : AbstractValidator<ProjectDto>
{
    public ProjectDtoValidator()
    {
        RuleFor(u => u.Name)!.NotNull();
        RuleFor(u => u.Engine)!.NotNull();
    }
}