using AutoMapper;
using Squirrel.Core.Common.DTO.Branch;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.MappingProfiles;

public sealed class BranchProfile : Profile
{
    public BranchProfile()
    {
        CreateMap<Branch, BranchDto>()!.ReverseMap();
    }
}