using AutoMapper;
using Squirrel.ConsoleApp.Models;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.DTO.View;
using Squirrel.Shared.Enums;
using static Squirrel.SqlService.BLL.Extensions.MappingExtensions;

namespace Squirrel.SqlService.BLL.MappingProfiles;

public sealed class ViewProfile : Profile
{
    public ViewProfile()
    {
        CreateMap<QueryResultTable, ViewNamesDto>()
            .ForMember(dest => dest.Views, opt
                => opt.MapFrom(src =>
                    src.Rows.Any()
                        ? src.Rows.Select(r => new View { Name = r[1], Schema = r[0] })
                        : Enumerable.Empty<View>()));

        CreateMap<QueryResultTable, ViewDetailsDto>()
            .ForMember(dest => dest.Details, opt
                => opt.MapFrom(src =>
                    src.Rows.Any()
                        ? src.Rows.Select(row => MapToObject<ViewDetailInfo>(src.ColumnNames.Select(c => c.ToLower()).ToList(), row))
                        : Enumerable.Empty<ViewDetailInfo>()));

        CreateMap<View, DatabaseItem>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => DatabaseItemType.View));
    }
}