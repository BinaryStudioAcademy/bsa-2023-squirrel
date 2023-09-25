using Squirrel.Shared.DTO.ConsoleAppHub;
using Squirrel.Shared.DTO.Table;

namespace Squirrel.Core.BLL.Interfaces;

public interface ITableService
{
    Task<TableNamesDto> GetTablesName(QueryParameters queryParameters);
}