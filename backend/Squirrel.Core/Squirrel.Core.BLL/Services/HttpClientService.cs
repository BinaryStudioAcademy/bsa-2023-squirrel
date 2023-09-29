using System.Net.Http.Json;
using Squirrel.Core.BLL.Extensions;
using Squirrel.Core.BLL.Interfaces;
using HttpRequestException = Squirrel.Shared.Exceptions.HttpRequestException;

namespace Squirrel.Core.BLL.Services;

public sealed class HttpClientService : IHttpClientService
{
    private readonly HttpClient _httpClient;

    public HttpClientService()
    {
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromMinutes(3);
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

    public async Task<TResponse> SendAsync<TRequest, TResponse>(string requestUrl, TRequest requestData, HttpMethod method)
    {
        var content = requestData is not null ? JsonContent.Create(requestData) : null;
        var message = new HttpRequestMessage { RequestUri = new Uri(requestUrl), Content = content, Method = method };
        var response = await _httpClient.SendAsync(message);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(errorMessage);
        }

        return await response.GetModelAsync<TResponse>();
    }

    public async Task SendAsync<TRequest>(string requestUrl, TRequest requestData, HttpMethod method)
    {
        var content = requestData is not null ? JsonContent.Create(requestData) : null;
        var message = new HttpRequestMessage { RequestUri = new Uri(requestUrl), Content = content, Method = method };
        var response = await _httpClient.SendAsync(message);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(errorMessage);
        }
    }
}
