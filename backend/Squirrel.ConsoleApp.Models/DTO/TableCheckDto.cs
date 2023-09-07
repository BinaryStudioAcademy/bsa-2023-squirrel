namespace Squirrel.ConsoleApp.Models.DTO
{
    public class TableChecksDto
    {
        public string Schema { get; set; } = null!;
        public string TableName { get; set; } = null!;
        public List<CheckRow> Rows { get; set; } = new();
    }
}
