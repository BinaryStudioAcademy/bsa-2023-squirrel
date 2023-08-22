using Microsoft.AspNetCore.Mvc;
using Squirrel.AzureBlobStorage.WebApi.Interfaces;
using Squirrel.AzureBlobStorage.WebApi.Models;

namespace Squirrel.AzureBlobStorage.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AzureBlobStorageController : ControllerBase
    {
        private readonly IBlobStorageService _storageService;
        public AzureBlobStorageController(IBlobStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpGet]
        public async Task<ActionResult<Blob>> Get(string containerName, string blobId)
        {
            return Ok(await _storageService.DownloadAsync(containerName, blobId));
        }

        [HttpPost]
        public async Task<ActionResult<Blob>> Add(string containerName, [FromBody] Blob blob)
        {
            return Ok(await _storageService.UploadAsync(containerName, blob));
        }

        [HttpPut]
        public async Task<ActionResult<Blob>> Update(string containerName, [FromBody] Blob blob)
        {
            return Ok(await _storageService.UpdateAsync(containerName, blob));
        }

        [HttpDelete]
        public async Task<ActionResult<Blob>> Delete(string containerName, string blobId)
        {
            if(await _storageService.DeleteAsync(containerName, blobId))
            {
                return NoContent();
            }
            return NotFound();            
        }
    }
}