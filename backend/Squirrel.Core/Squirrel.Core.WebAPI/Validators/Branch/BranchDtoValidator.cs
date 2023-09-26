using FluentValidation;
using Squirrel.Core.Common.DTO.Branch;

namespace Squirrel.Core.WebAPI.Validators.Branch;

public class BranchDtoValidator : AbstractValidator<BranchDto>
{
    public const string BranchNameRegex = @"^[\w\-@]+$";
    
    public BranchDtoValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3)
            .MaximumLength(200)
            .Matches(BranchNameRegex);
    }
}