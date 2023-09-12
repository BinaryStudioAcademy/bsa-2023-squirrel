using FluentValidation;
using Squirrel.Core.Common.DTO.Project;

namespace Squirrel.Core.WebAPI.Validators.Project;

public class UpdateProjectDtoValidator: AbstractValidator<UpdateProjectDto>
{
    public UpdateProjectDtoValidator()
    {
        RuleFor(x => x.Name)!.MaximumLength(1000);
        RuleFor(x => x.Name)!
            .MinimumLength(3)!
            .MaximumLength(50);
    }
}
