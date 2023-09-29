namespace Squirrel.Shared.DTO.SelectedItems;

public class SelectedItemsDto
{
    public string Guid { get; set; } = null!;
    public int CommitId { get; set; }
    public List<string> StoredProcedures { get; set; } = new();
    public List<string> Tables { get; set; } = new();
    public List<string> Types { get; set; } = new();
    public List<string> Functions { get; set; } = new();
    public List<string> Constraints { get; set; } = new();
    public List<string> Views { get; set; } = new();
}
