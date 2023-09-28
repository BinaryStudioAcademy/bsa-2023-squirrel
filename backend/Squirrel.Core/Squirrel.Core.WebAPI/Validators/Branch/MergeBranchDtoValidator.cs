using FluentValidation;
using Squirrel.Core.Common.DTO.Branch;

namespace Squirrel.Core.WebAPI.Validators.Branch;

public class MergeBranchDtoValidator: AbstractValidator<MergeBranchDto>
{
    public MergeBranchDtoValidator()
    {
        RuleFor(x => x.SourceId)
            .NotEqual(x => x.DestinationId);

        RuleFor(x => x.DestinationId)
            .NotEqual(x => x.SourceId);
    }
}
