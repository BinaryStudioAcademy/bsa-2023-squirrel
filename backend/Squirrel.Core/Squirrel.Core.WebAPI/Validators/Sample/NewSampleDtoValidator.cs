using FluentValidation;
using Squirrel.Core.Common.DTO.Sample;

namespace Squirrel.Core.WebAPI.Validators.Sample;

public class NewSampleDtoValidator : AbstractValidator<SampleDto>
{
    public NewSampleDtoValidator()
    {
        RuleFor(u => u.Title).NotNull().MaximumLength(50);
        RuleFor(u => u.CreatedBy).NotEmpty();
    }
}