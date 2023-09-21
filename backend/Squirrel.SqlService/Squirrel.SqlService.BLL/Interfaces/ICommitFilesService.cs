using Squirrel.Shared.DTO.CommitFile;
using Squirrel.Shared.DTO.SelectedItems;
using Squirrel.SqlService.BLL.Models.DTO;

namespace Squirrel.SqlService.BLL.Interfaces;
public interface ICommitFilesService
{
    Task<ICollection<CommitFileDto>> SaveSelectedFiles(SelectedItemsDto selectedItems);
    DbStructureDto GetTestStructure();
}
