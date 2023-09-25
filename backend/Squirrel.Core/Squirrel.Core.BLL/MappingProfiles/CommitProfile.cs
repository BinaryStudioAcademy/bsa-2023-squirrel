using AutoMapper;
using Squirrel.Core.Common.DTO.Commit;
using Squirrel.Core.DAL.Entities;
using Squirrel.Shared.DTO.CommitFile;

namespace Squirrel.Core.BLL.MappingProfiles;
public class CommitProfile: Profile
{
    public CommitProfile()
    {
        CreateMap<Commit, CommitDto>();
        CreateMap<CommitFileDto, CommitFile>().ReverseMap();
        CreateMap<CreateCommitDto, Commit>();
    }
}
