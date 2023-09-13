using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Squirrel.AzureBlobStorage.Models;
using Squirrel.Core.Common.DTO.Users;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.MappingProfiles;

public sealed class UserProfile : Profile
{
    public UserProfile(BlobServiceClient blobServiceClient, IOptions<BlobStorageOptions> blobStorageOptions)
    {
        CreateMap<User, UserDto>()!.ReverseMap();
        CreateMap<User, UserProfileDto>()
            .ForMember(dest =>
                dest.AvatarUrl, opt =>
                opt.MapFrom(src =>
                    src.AvatarUrl != null
                        ? $"{blobServiceClient.Uri.AbsoluteUri}/{blobStorageOptions.Value.ImagesContainer}/{src.AvatarUrl}"
                        : null))
            .ReverseMap();
    }
}