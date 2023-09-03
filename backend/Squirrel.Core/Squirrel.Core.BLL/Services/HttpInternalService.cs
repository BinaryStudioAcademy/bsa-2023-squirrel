using Squirrel.Core.BLL.Interfaces;
using System.Net.Http.Json;

namespace Squirrel.Core.BLL.Services;

public class HttpInternalService : IHttpInternalService
{
    private readonly HttpClient _httpClient;

    public HttpInternalService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<TResponse> GetAsync<TResponse>(string requestUrl)
    {
        // Send a GET request to the url.
        HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

        if (response.IsSuccessStatusCode)
        {
            // Read and parse the response content as a list of TResponse objects.
            TResponse? databaseItems = await response.Content.ReadFromJsonAsync<TResponse>();

            // Return the TResponse
            return databaseItems!;
        }
        else
        {
            // Throw HTTP error responses here.
            throw new HttpRequestException($"HTTP Error: {response.StatusCode}");
        }
    }
}
