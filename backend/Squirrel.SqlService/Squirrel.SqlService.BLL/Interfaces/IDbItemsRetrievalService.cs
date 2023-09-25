using Squirrel.Shared.DTO;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.DTO.Function;
using Squirrel.Shared.DTO.Procedure;
using Squirrel.Shared.DTO.Table;
using Squirrel.SqlService.BLL.Models.DTO;
using Squirrel.SqlService.BLL.Models.DTO.View;

namespace Squirrel.SqlService.BLL.Interfaces;

public interface IDbItemsRetrievalService
{
    Task<ICollection<DatabaseItem>> GetAllItemsAsync(Guid clientId);

    Task<DbStructureDto> GetAllDbStructureAsync(Guid clientId);

    Task<TableNamesDto> GetTableNamesAsync(Guid clientId);

    Task<ICollection<TableStructureDto>> GetAllTableStructuresAsync(Guid clientId);

    Task<ICollection<TableConstraintsDto>> GetAllTableConstraintsAsync(Guid clientId);

    Task<FunctionNamesDto> GetFunctionsNamesAsync(Guid clientId);

    Task<FunctionDetailsDto> GetAllFunctionDetailsAsync(Guid clientId);

    Task<ProcedureNamesDto> GetProceduresNamesAsync(Guid clientId);

    Task<ProcedureDetailsDto> GetAllProcedureDetailsAsync(Guid clientId);

    Task<ViewNamesDto> GetViewNamesAsync(Guid clientId);

    Task<ViewDetailsDto> GetAllViewDetailsAsync(Guid clientId);
}

