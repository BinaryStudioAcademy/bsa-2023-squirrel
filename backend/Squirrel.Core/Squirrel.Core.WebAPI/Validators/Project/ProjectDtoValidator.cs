using FluentValidation;
using Squirrel.Core.Common.DTO.Project;
using System.Text.RegularExpressions;

namespace Squirrel.Core.WebAPI.Validators.Project;

public class ProjectDtoValidator : AbstractValidator<ProjectDto>
{
    public ProjectDtoValidator()
    {
        RuleFor(x => x.Description)!.MaximumLength(1000);
        RuleFor(x => x.DbEngine)!.NotNull();
        RuleFor(x => x.Name)!
            .MinimumLength(3)!
            .MaximumLength(50)
            .Matches(ValidationRegExes.NoCyrillic);
    }
}