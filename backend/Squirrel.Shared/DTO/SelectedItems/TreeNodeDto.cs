namespace Squirrel.Shared.DTO.SelectedItems;
public class TreeNodeDto
{
    public string Name { get; set; } = null!;
    public List<TreeNodeDto> Children { get; set; } = new List<TreeNodeDto>();
    public bool Selected { get; set; }
}
