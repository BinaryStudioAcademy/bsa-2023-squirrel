using FluentValidation;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Validators;

public class ConnectionStringDtoValidator : AbstractValidator<ConnectionStringDto>
{
    public ConnectionStringDtoValidator()
    {
        RuleFor(u => u.DbEngine).IsInEnum();
    }
}