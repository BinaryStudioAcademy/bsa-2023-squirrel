namespace Squirrel.ConsoleApp.Models.DTO
{
    public class CheckRow
    {
        public string ConstraintName { get; set; } = null!;
        public string Columns { get; set; } = null!;
        public string CheckClause { get; set; } = null!;
    }
}
