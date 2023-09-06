using FluentValidation;
using Squirrel.ConsoleApp.Models;

namespace Squirrel.Core.WebAPI.Validators.Project;

public class DbSettingsValidator : AbstractValidator<DbSettings>
{
    public DbSettingsValidator()
    {
        RuleFor(u => u.DbType).IsInEnum();
    }
}