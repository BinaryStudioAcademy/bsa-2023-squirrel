using System.Text;

namespace Squirrel.Core.Common.DTO.SquirrelHub;

public class QueryResultTableDTO
{
    public string[] ColumnNames { get; set; }
    public List<string[]> Rows { get; set; }
    public int RowCount { get; set; }
    public int ColumnCount { get; set; }

    // Just for debugging and demo
    public override string ToString()
    {
        var res = new StringBuilder(string.Join("  |  ", ColumnNames));
        res.Append("\n---");
        foreach (var item in Rows)
        {
            res.AppendLine("\n" + string.Join("  |  ", item));
        }
       
        return res.ToString();
    }
}
