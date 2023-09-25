using Squirrel.Shared.DTO;
using Squirrel.Shared.DTO.CommitFile;
using Squirrel.Shared.DTO.SelectedItems;

namespace Squirrel.SqlService.BLL.Interfaces;
public interface ICommitFilesService
{
    Task<ICollection<CommitFileDto>> SaveSelectedFiles(SelectedItemsDto selectedItems);
    DbStructureDto GetTestStructure();
}
