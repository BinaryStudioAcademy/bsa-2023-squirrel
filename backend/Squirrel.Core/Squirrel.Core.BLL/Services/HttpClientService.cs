﻿using System.Net.Http.Json;
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
        // Serialize the request data to JSON (assuming you're sending JSON).
        var content = requestData != null ? JsonContent.Create(requestData) : null;

        var message = new HttpRequestMessage { RequestUri = new Uri(requestUrl), Content = content, Method = method };

        // Send a request to the URL with the serialized data.
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
        // Serialize the request data to JSON (assuming you're sending JSON).
        var content = requestData != null ? JsonContent.Create(requestData) : null;

        var message = new HttpRequestMessage { RequestUri = new Uri(requestUrl), Content = content, Method = method };

        // Send a request to the URL with the serialized data.
        var response = await _httpClient.SendAsync(message);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(errorMessage);
        }
    }
}
