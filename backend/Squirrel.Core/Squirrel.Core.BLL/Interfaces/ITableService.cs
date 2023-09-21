using Squirrel.Core.Common.DTO.SqlService;
using Squirrel.Core.Common.DTO.Table;

namespace Squirrel.Core.BLL.Interfaces;

public interface ITableService
{
    Task<TableNamesDto> GetTablesName(QueryParameters queryParameters);
}