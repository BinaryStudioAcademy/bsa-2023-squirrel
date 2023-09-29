using AutoMapper;
using Squirrel.ConsoleApp.Models;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.DTO.Procedure;
using Squirrel.Shared.Enums;
using static Squirrel.SqlService.BLL.Extensions.MappingExtensions;


namespace Squirrel.SqlService.BLL.MappingProfiles;

public sealed class ProcedureProfile : Profile
{
    public ProcedureProfile()
    {
        CreateMap<QueryResultTable, ProcedureNamesDto>()!
            .ForMember(dest => dest.Procedures, opt
                => opt.MapFrom(src =>
                    src.Rows.Any()
                        ? src.Rows.Select(r => new Procedure { Name = r[1], Schema = r[0] })
                        : Enumerable.Empty<Procedure>()));

        CreateMap<QueryResultTable, ProcedureDetailsDto>()!
            .ForMember(dest => dest.Details, opt
                => opt.MapFrom(src =>
                    src.Rows.Any()
                        ? src.Rows.Select(row => MapToObject<ProcedureDetailInfo>(src.ColumnNames.Select(c => c.ToLower()).ToList(), row))
                        : Enumerable.Empty<ProcedureDetailInfo>()));

        CreateMap<Procedure, DatabaseItem>()!
           .ForMember(dest => dest.Type, opt => opt.MapFrom(src => DatabaseItemType.StoredProcedure));
    }
}