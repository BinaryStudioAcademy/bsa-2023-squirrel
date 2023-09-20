using AutoMapper;
using Squirrel.Core.Common.DTO.Commit;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.MappingProfiles;
public class CommitProfile: Profile
{
    public CommitProfile()
    {
        CreateMap<Commit, CommitDto>();
    }
}
