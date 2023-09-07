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
                .ForMember(dest => dest.Tables, opt => opt.MapFrom(src => src.Rows.Select(row => new Table
                {
                    Name = row[0].Split(new[] { '.' }).Last(),
                    Schema = row[0].Split(new[] { '.' }).First()
                })));

            CreateMap<QueryResultTable, TableStructureDto>()
                .ForMember(dest => dest.Schema, opt => opt.MapFrom(src => src.Rows[0][0]))
                .ForMember(dest => dest.TableName, opt => opt.MapFrom(src => src.Rows[0][1]))
                .ForMember(dest => dest.Rows, opt => opt.MapFrom(src => src.Rows.Select(row => MapToRow(src.ColumnNames, row))));
        }
    }
}
