using FluentValidation;
using Squirrel.Core.Common.DTO.Project;

namespace Squirrel.Core.WebAPI.Validators.Project;

public class ProjectDtoValidator : AbstractValidator<ProjectDto>
{
    public ProjectDtoValidator()
    {
        RuleFor(x => x.Description)!.MaximumLength(1000);
        RuleFor(x => x.DbEngine)!.NotNull();
        RuleFor(x => x.Name)!
            .NotNull()!
            .MinimumLength(3)!
            .MaximumLength(50);
    }
}