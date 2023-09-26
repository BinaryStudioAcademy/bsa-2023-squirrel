using FluentValidation;
using SixLabors.ImageSharp.ColorSpaces;
using Squirrel.Core.Common.DTO.Commit;

namespace Squirrel.Core.WebAPI.Validators.Commit;

public class CreateCommitDtoValidator: AbstractValidator<CreateCommitDto>
{
    public CreateCommitDtoValidator()
    {
        RuleFor(x => x.Message)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(x => x.ChangesGuid)
            .NotEmpty();

        RuleFor(x => x.SelectedItems)
            .Must(x => x.Any(x=> x.Children.Any(y => y.Selected)));
    }
}
