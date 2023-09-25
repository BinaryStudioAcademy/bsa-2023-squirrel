using AutoMapper;
using Squirrel.ConsoleApp.Models;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.DTO.UserDefinedType.DataType;
using Squirrel.Shared.DTO.UserDefinedType.TableType;
using Squirrel.Shared.Enums;
using static Squirrel.SqlService.BLL.Extensions.MappingExtensions;

namespace Squirrel.SqlService.BLL.MappingProfiles;

public class UserDefinedTypeProfile: Profile
{
    public UserDefinedTypeProfile()
    {
        CreateMap<QueryResultTable, UserDefinedDataTypeDetailsDto>()
            .ForMember(dest => dest.Details, opt 
                => opt.MapFrom(src => 
                    src.Rows.Any()
                    ? src.Rows.Select(row => MapToObject<UserDefinedDataTypeDetailInfo>(src.ColumnNames.Select(c => c.ToLower()).ToList(), row))
                    : Enumerable.Empty<UserDefinedDataTypeDetailInfo>()));

        CreateMap<UserDefinedDataType, DatabaseItem>()
            .ForMember(dest => dest.Type, opt 
                => opt.MapFrom(src => DatabaseItemType.UserDefinedDataType));

        CreateMap<QueryResultTable, UserDefinedTables>()
            .ForMember(dest => dest.Tables, opt
                => opt.MapFrom(src
                    => src.Rows.Any()
                        ? MapToUdtTables(src.ColumnNames.Select(c => c.ToLower()).ToList(), src.Rows)
                        : Enumerable.Empty<UserDefinedTableDetailsDto>()));
        
        CreateMap<UserDefinedTableType, DatabaseItem>()
            .ForMember(dest => dest.Type, opt 
                => opt.MapFrom(src => DatabaseItemType.UserDefinedTableType));
    }
}