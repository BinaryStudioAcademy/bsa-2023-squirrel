using AutoMapper;
using Squirrel.Core.Common.DTO.Sample;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.MappingProfiles;

public sealed class SampleProfile : Profile
{
    public SampleProfile()
    {
        CreateMap<Sample, SampleDto>();
        CreateMap<SampleDto, Sample>();
        CreateMap<NewSampleDto, Sample>();
    }
}