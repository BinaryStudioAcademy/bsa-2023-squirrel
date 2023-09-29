using Squirrel.Shared.DTO.Table;

namespace Squirrel.SqlService.BLL.Interfaces;

public interface ICreateTableScriptService
{
    string GenerateCreateTableScript(string tableSchema, string tableName, TableStructureDto tableStructure, List<string> fkConstraintScripts);
}
