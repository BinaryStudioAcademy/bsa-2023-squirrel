using Squirrel.Core.DAL.Enums;
using Squirrel.Core.Common.DTO.Script;

namespace Squirrel.SqlService.BLL.Interfaces;

public interface ISqlFormatterService
{
    ScriptContentDto GetFormattedSql(DbEngine dbEngine, string inputSql);
}
