namespace Squirrel.Shared.DTO.SelectedItems;
public class SelectedItemsDto
{
    public string Guid { get; set; } = null!;
    public int CommitId { get; set; }
    public ICollection<string> StoredProcedures { get; set; } = new List<string>();
    public ICollection<string> Tables { get; set; } = new List<string>();
    public ICollection<string> Types { get; set; } = new List<string>();
    public ICollection<string> Functions { get; set; } = new List<string>();
    public ICollection<string> Constraints { get; set; } = new List<string>();
}
