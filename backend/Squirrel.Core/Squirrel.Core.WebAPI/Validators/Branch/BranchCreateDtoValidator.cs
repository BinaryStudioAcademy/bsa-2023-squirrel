using FluentValidation;
using Squirrel.Core.Common.DTO.Branch;
using System.Text.RegularExpressions;

namespace Squirrel.Core.WebAPI.Validators.Branch;

public class BranchCreateDtoValidator: AbstractValidator<BranchCreateDto>
{
    public BranchCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3)
            .MaximumLength(200)
            .Must(x => Regex.IsMatch(x, @"^[\w\-@]+$"));
    }
}
