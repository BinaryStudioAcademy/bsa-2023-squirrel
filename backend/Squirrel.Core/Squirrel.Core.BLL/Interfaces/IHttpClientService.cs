﻿namespace Squirrel.Core.BLL.Interfaces;

public interface IHttpClientService
{
    Task<TResponse> GetAsync<TResponse>(string requestUrl);
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    Task<TResponse> PostAsync<TRequest, TResponse>(string requestUrl, TRequest requestData);
}
