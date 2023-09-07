using AutoMapper;
using Squirrel.ConsoleApp.Models;
using Squirrel.ConsoleApp.Models.DTO;

namespace Squirrel.ConsoleApp.BL.MappingProfiles
{
    public sealed class TableProfile : Profile
    {
        public TableProfile()
        {
            CreateMap<QueryResultTable, TableDto>();
        }
    }
}
