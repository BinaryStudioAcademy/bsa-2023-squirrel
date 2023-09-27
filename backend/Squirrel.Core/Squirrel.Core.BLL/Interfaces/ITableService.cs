using Squirrel.Shared.DTO.ConsoleAppHub;
using Squirrel.Shared.DTO.Table;

namespace Squirrel.Core.BLL.Interfaces;

public interface ITableService
{
    Task<TableNamesDto> GetTablesNameAsync(QueryParameters queryParameters);
    Task<TableStructureDto> GetTableStructureAsync(QueryParameters queryParameters);
}