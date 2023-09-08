namespace Squirrel.ConsoleApp.Models.DTO
{
    public class TableDataDto
    {
        public string Schema { get; set; } = null!;
        public string TableName { get; set; } = null!;
        public int TotalRows { get; set; } = 0!;
        public List<Dictionary<string, string>> Rows { get; set; } = new();
    }
}
