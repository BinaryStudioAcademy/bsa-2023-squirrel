﻿using FluentValidation;
using Squirrel.Core.Common.DTO.Branch;
using System.Text.RegularExpressions;

namespace Squirrel.Core.WebAPI.Validators.Branch;

public class BranchDtoValidator : AbstractValidator<BranchDto>
{
    public BranchDtoValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3)
            .MaximumLength(200)
            .Matches(@"^[\w\-@]+$");
    }
}