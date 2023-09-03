using Microsoft.AspNetCore.Mvc;
using Squirrel.Shared.DTO.DatabaseItem;

namespace Squirrel.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DatabaseItemsController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<DatabaseItem>>> GetAllItems()
    {
        using (var httpClient = new HttpClient())
        {
            // Replace with the actual URL of your microservice's DatabaseItemsController.
            string sqlMicroservice = "https://localhost:7244/databaseitems";

            // Send a GET request to the microservice.
            HttpResponseMessage response = await httpClient.GetAsync(sqlMicroservice);

            if (response.IsSuccessStatusCode)
            {
                // Read and parse the response content as a list of DatabaseItem objects.
                List<DatabaseItem>? databaseItems = await response.Content.ReadFromJsonAsync<List<DatabaseItem>>();

                // Return the list as an ActionResult.
                return Ok(databaseItems);
            }
            else
            {
                // Handle HTTP error responses here.
                return StatusCode((int)response.StatusCode, "Failed to retrieve data from microservice.");
            }
        }
    }
}

