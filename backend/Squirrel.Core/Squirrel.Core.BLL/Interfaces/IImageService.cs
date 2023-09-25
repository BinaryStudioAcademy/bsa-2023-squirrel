using Microsoft.AspNetCore.Http;

namespace Squirrel.Core.BLL.Interfaces;

public interface IImageService
{
    Task AddAvatarAsync(IFormFile avatar);
    Task DeleteAvatarAsync();
}