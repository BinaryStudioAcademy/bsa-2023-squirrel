using Squirrel.Shared.Extensions;
using Squirrel.Shared.Interfaces;
using System.Net.Http.Json;

namespace Squirrel.Shared.Services;

public sealed class HttpClientService : IHttpClientService
{
    private readonly HttpClient _httpClient;

    public HttpClientService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<TResponse> GetAsync<TResponse>(string requestUrl)
    {
        var response = await _httpClient.GetAsync(requestUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"HTTP Error: {response.StatusCode}");
        }

        return await response.GetModelAsync<TResponse>();
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string requestUrl, TRequest requestData)
    {
        var content = JsonContent.Create(requestData);

        var response = await _httpClient.PostAsync(requestUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"HTTP Error: {response.StatusCode}");
        }

        return await response.GetModelAsync<TResponse>();
    }

    public async Task PostAsync<TRequest>(string requestUrl, TRequest requestData)
    {
        var content = JsonContent.Create(requestData);

        var response = await _httpClient.PostAsync(requestUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"HTTP Error: {response.StatusCode}");
        }
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        => await _httpClient.SendAsync(request).ConfigureAwait(false);
}
