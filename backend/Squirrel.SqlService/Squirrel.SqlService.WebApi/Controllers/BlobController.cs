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
    public async Task<ActionResult<Blob>> GetBlob(string containerName, string blobId)
    {
        return Ok(await _blobService.DownloadAsync(containerName, blobId));
    }

    [HttpPost("{containerName}")]
    public async Task<ActionResult<Blob>> UploadBlob(string containerName, [FromBody] Blob blob)
    {
        return Ok(await _blobService.UploadAsync(containerName, blob));
    }

    [HttpPut("{containerName}")]
    public async Task<ActionResult<Blob>> UpdateBlob(string containerName, [FromBody] Blob blob)
    {
        return Ok(await _blobService.UpdateAsync(containerName, blob));
    }

    [HttpDelete("{containerName}/{blobId}")]
    public async Task<ActionResult<Blob>> DeleteBlob(string containerName, string blobId)
    {
        await _blobService.DeleteAsync(containerName, blobId);
        return NoContent();
    }
}
