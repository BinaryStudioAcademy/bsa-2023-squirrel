using Squirrel.SqlService.BLL.Models.ConsoleAppHub;
using Squirrel.SqlService.BLL.Models.DTO;

namespace Squirrel.Core.BLL.Interfaces;

public interface ITableService
{
    Task<TableNamesDto> GetTablesName(QueryParameters queryParameters);
}