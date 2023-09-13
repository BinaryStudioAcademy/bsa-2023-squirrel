using FluentValidation;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.Core.WebAPI.Validators.Project;

public class ConnectionStringDtoValidator : AbstractValidator<ConnectionStringDto>
{
    public ConnectionStringDtoValidator()
    {
        RuleFor(u => u.DbEngine).IsInEnum();
    }
}