using FluentValidation;
using Squirrel.Core.Common.DTO.Branch;

namespace Squirrel.Core.WebAPI.Validators.Branch;

public class BranchUpdateDtoValidator : AbstractValidator<BranchUpdateDto>
{
    public BranchUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3)
            .MaximumLength(200)
            .Matches(BranchDtoValidator.BranchNameRegex);
    }
}