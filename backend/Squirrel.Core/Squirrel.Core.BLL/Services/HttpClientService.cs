using Squirrel.Core.BLL.Extensions;
using Squirrel.Core.BLL.Interfaces;

namespace Squirrel.Core.BLL.Services;

public sealed class HttpClientService : IHttpClientService
{
    private readonly HttpClient _httpClient;

    public HttpClientService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<TResponse> GetAsync<TResponse>(string requestUrl)
    {
        // Send a GET request to the url.
        var response = await _httpClient.GetAsync(requestUrl);

        if (!response.IsSuccessStatusCode)
        {
            // Throw HTTP error responses here.
            throw new HttpRequestException($"HTTP Error: {response.StatusCode}");
        }

        // Return the TResponse
        return await response.GetModelAsync<TResponse>();
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        => await _httpClient.SendAsync(request).ConfigureAwait(false);
}
