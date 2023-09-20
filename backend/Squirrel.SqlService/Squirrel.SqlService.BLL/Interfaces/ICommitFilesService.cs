using Squirrel.Shared.DTO.SelectedItems;

namespace Squirrel.SqlService.BLL.Interfaces;
public interface ICommitFilesService
{
    Task SaveSelectedFiles(SelectedItemsDto selectedItems);
}
