using AutoMapper;
using Squirrel.Core.Common.DTO.Branch;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.MappingProfiles;

public sealed class BranchProfile : Profile
{
    public BranchProfile()
    {
        CreateMap<Branch, BranchDto>()!.ReverseMap();
        CreateMap<Branch, BranchCreateDto>()!.ReverseMap();
        CreateMap<Branch, BranchDetailsDto>()
            .ForMember(x => x.LastUpdatedBy, s => s.MapFrom(x => x.Author))
            .ForMember(x => x.UpdatedAt, s => s.MapFrom(x => x.CreatedAt));
    }
}