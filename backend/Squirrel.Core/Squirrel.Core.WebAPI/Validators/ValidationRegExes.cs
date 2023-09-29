namespace Squirrel.Core.WebAPI.Validators;

public static class ValidationRegExes
{
    public const string NoCyrillic = @"^(?![\u0410-\u044F\u0400-\u04FF]).*$";

    public const string BranchName = @"^(?!.*[-_]$)(?![_@-])(?!.*[-_]{2,})[A-Za-z@_-]+(?![_@-])$";
}
