using Microsoft.AspNetCore.Mvc;
using Squirrel.AzureBlobStorage.Interfaces;
using Squirrel.AzureBlobStorage.Models;

namespace Squirrel.SqlService.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlobController : ControllerBase
{
    private readonly IBlobStorageService _blobService;

    public BlobController(IBlobStorageService blobService)
    {
        _blobService = blobService;
    }

    [HttpGet("{containerName}/{blobId}")]
    public async Task<ActionResult<Blob>> GetBlobAsync(string containerName, string blobId)
    {
        return Ok(await _blobService.DownloadAsync(containerName, blobId));
    }

    [HttpPost("{containerName}")]
    public async Task<ActionResult> UploadBlobAsync(string containerName, [FromBody] Blob blob)
    {
        await _blobService.UploadAsync(containerName, blob);
        return Ok();
    }

    [HttpPut("{containerName}")]
    public async Task<ActionResult> UpdateBlobAsync(string containerName, [FromBody] Blob blob)
    {
        await _blobService.UpdateAsync(containerName, blob);
        return Ok();
    }

    [HttpDelete("{containerName}/{blobId}")]
    public async Task<ActionResult> DeleteBlobAsync(string containerName, string blobId)
    {
        await _blobService.DeleteAsync(containerName, blobId);
        return NoContent();
    }
}