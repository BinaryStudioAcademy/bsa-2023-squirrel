namespace Squirrel.Shared.DTO.SelectedItems;
public class SelectedItemsDto
{
    public string Guid { get; set; } = null!;
    public List<TreeNodeDto> SelectedItems { get; set; }
}
