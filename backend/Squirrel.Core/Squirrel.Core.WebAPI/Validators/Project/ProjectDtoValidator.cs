using FluentValidation;
using Squirrel.Core.Common.DTO.Project;

namespace Squirrel.Core.WebAPI.Validators.Project;

public class ProjectDtoValidator : AbstractValidator<ProjectDto>
{
    public ProjectDtoValidator()
    {
        RuleFor(u => u.Name)!.NotNull();
        RuleFor(u => u.DbEngine)!.NotNull();
    }
}