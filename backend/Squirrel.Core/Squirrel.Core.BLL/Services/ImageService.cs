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
        ValidateImage(avatar);

        var userEntity = await GetUserByIdInternal(_userIdGetter.GetCurrentUserId());

        var content = await CropAvatar(avatar);
        var guid = userEntity.AvatarUrl ?? Guid.NewGuid().ToString();
        var blob = new Blob
        {
            Id = guid,
            ContentType = avatar.ContentType,
            Content = content
        };

        await (userEntity.AvatarUrl == null
            ? _blobStorageService.UploadAsync(_blobStorageOptions.ImagesContainer, blob)
            : _blobStorageService.UpdateAsync(_blobStorageOptions.ImagesContainer, blob));

        userEntity.AvatarUrl = guid;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAvatarAsync()
    {
        var userEntity = await GetUserByIdInternal(_userIdGetter.GetCurrentUserId());
        if (userEntity.AvatarUrl == null)
        {
            throw new EntityNotFoundException(nameof(User.AvatarUrl));
        }

        await _blobStorageService
            .DeleteAsync(_blobStorageOptions.ImagesContainer, userEntity.AvatarUrl);

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

    private void ValidateImage(IFormFile image)
    {
        if (!_fileTypes.Contains(image.ContentType))
        {
            throw new InvalidFileFormatException(string.Join(", ", _fileTypes));
        }

        if (image.Length > MaxFileLenght)
        {
            throw new LargeFileException($"{MaxFileLenght / (1024 * 1024)} MB");
        }
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