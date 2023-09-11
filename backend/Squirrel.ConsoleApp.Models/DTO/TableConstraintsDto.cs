namespace Squirrel.ConsoleApp.Models.DTO;

public class TableConstraintsDto
{
    public string Schema { get; set; } = null!;
    public string TableName { get; set; } = null!;
    public List<Constraint> Constraints { get; set; } = new();
}