using FluentValidation;
using Squirrel.Core.Common.DTO.Project;
using System.Text.RegularExpressions;

namespace Squirrel.Core.WebAPI.Validators.Project;

public class UpdateProjectDtoValidator: AbstractValidator<UpdateProjectDto>
{
    public UpdateProjectDtoValidator()
    {
        RuleFor(x => x.Description)!.MaximumLength(1000);
        RuleFor(x => x.Name)!
            .MinimumLength(3)!
            .MaximumLength(50)
            .Must(x => Regex.IsMatch(x, @"^(?![\u0410-\u044F\u0400-\u04FF]).*$"));
    }
}
