using Squirrel.Core.DAL.Enums;
using Squirrel.SqlService.BLL.Models.DTO.Script;

namespace Squirrel.SqlService.BLL.Interfaces;

public interface ISqlFormatterService
{
    ScriptContentDto GetFormattedSql(DbEngine dbEngine, string inputSql);
}
