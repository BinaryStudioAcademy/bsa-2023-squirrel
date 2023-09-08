using AutoMapper;
using Squirrel.ConsoleApp.Models;
using Squirrel.ConsoleApp.Models.DTO;
using static Squirrel.ConsoleApp.BL.Extensions.MappingExtensions;

namespace Squirrel.ConsoleApp.BL.MappingProfiles
{
    public sealed class TableProfile : Profile
    {
        public TableProfile()
        {
            CreateMap<QueryResultTable, TableNamesDto>()
                .ForMember(dest => dest.Tables, opt => opt.MapFrom(src => src.Rows.Select(row => new Table {Schema = row[0], Name = row[1]})));

            CreateMap<QueryResultTable, TableStructureDto>()
                .ForMember(dest => dest.Schema, opt => opt.MapFrom(src => src.Rows[0][0]))
                .ForMember(dest => dest.TableName, opt => opt.MapFrom(src => src.Rows[0][1]))
                .ForMember(dest => dest.Columns, opt => opt.MapFrom(src => src.Rows.Select(row => MapToStructureRow(src.ColumnNames, row))));

            CreateMap<QueryResultTable, TableConstraintsDto>()
                .ForMember(dest => dest.Schema, opt => opt.MapFrom(src => src.Rows[0][0]))
                .ForMember(dest => dest.TableName, opt => opt.MapFrom(src => src.Rows[0][1]))
                .ForMember(dest => dest.Constraints, opt => opt.MapFrom(src => src.Rows.Select(row => MapToChecksRow(src.ColumnNames, row))));

            CreateMap<QueryResultTable, TableDataDto>()
                .ForMember(dest => dest.Schema, opt => opt.MapFrom(src => src.Rows[0][0]))
                .ForMember(dest => dest.TableName, opt => opt.MapFrom(src => src.Rows[0][1]))
                .ForMember(dest => dest.TotalRows, opt => opt.MapFrom(src => int.Parse(src.Rows[0][2])))
                .ForMember(dest => dest.Rows, opt => opt.MapFrom(src => src.Rows.Select(row => MapToDataRow(src.ColumnNames, row))));
        }
    }
}
