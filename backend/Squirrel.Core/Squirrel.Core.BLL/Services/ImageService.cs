using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp.Formats.Jpeg;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.AzureBlobStorage.Models;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;
using Squirrel.Shared.Exceptions;

namespace Squirrel.Core.BLL.Services;

public class ImageService : IImageService
{
    private const int MaxFileLenght = 5 * 1024 * 1024;
    private readonly string[] _fileTypes = { "image/png", "image/jpeg" };
    private readonly SquirrelCoreContext _context;
    private readonly IBlobStorageService _blobStorageService;
    private readonly IUserIdGetter _userIdGetter;
    private readonly BlobStorageOptions _blobStorageOptions;

    public ImageService(SquirrelCoreContext context, IBlobStorageService blobStorageService, IUserIdGetter userIdGetter,
        IOptions<BlobStorageOptions> blobStorageOptions)
    {
        _context = context;
        _blobStorageService = blobStorageService;
        _userIdGetter = userIdGetter;
        _blobStorageOptions = blobStorageOptions.Value;
    }

    public async Task AddAvatarAsync(IFormFile avatar)
    {
        if (!_fileTypes.Contains(avatar.ContentType))
        {
            throw new InvalidFileFormatException(string.Join(", ", _fileTypes));
        }

        if (avatar.Length > MaxFileLenght)
        {
            throw new LargeFileException("5 MB");
        }

        var userEntity = await GetUserByIdInternal(_userIdGetter.GetCurrentUserId());
        var content = await CropAvatar(avatar);

        var blob = new Blob
        {
            Id = userEntity.Id.ToString(),
            ContentType = avatar.ContentType,
            Content = content
        };

        userEntity.AvatarUrl = await _blobStorageService.UploadWithUrlAsync(_blobStorageOptions.ImagesContainer, blob);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAvatarAsync()
    {
        var userEntity = await GetUserByIdInternal(_userIdGetter.GetCurrentUserId());
        await _blobStorageService
            .DeleteAsync(_blobStorageOptions.ImagesContainer, userEntity.Id.ToString());

        userEntity.AvatarUrl = null;
        await _context.SaveChangesAsync();
    }

    private async Task<byte[]> CropAvatar(IFormFile avatar)
    {
        using var image = await Image.LoadAsync(avatar.OpenReadStream());

        var smallerDimension = Math.Min(image.Width, image.Height);
        image.Mutate(x => x.Crop(smallerDimension, smallerDimension));

        using var ms = new MemoryStream();
        await image.SaveAsync(ms, new JpegEncoder());
        return ms.ToArray();
    }

    private async Task<User> GetUserByIdInternal(int id)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (userEntity == null)
        {
            throw new EntityNotFoundException(nameof(User), id);
        }

        return userEntity;
    }
}