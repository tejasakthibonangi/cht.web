﻿using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Cht.HMS.Web.UI.Factory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly HttpClient _httpClient;

        public RepositoryFactory(HttpClientService httpClientService)
        {
            _httpClient = httpClientService.GetHttpClient();
        }


        public async Task<TResponse> SendAsync<TRequest, TResponse>(HttpMethod method, string uri, TRequest entity = default)
        {
            var requestMessage = new HttpRequestMessage(method, uri);

            if (entity != null)
            {
                var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
                requestMessage.Content = content;
            }

            var response = await _httpClient.SendAsync(requestMessage);
            return await HandleResponse<TResponse>(response);
        }

        public async Task<TResponse> SendAsync<TResponse>(HttpMethod method, string uri)
        {
            var requestMessage = new HttpRequestMessage(method, uri);
            var response = await _httpClient.SendAsync(requestMessage);
            return await HandleResponse<TResponse>(response);
        }

        private async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                if (typeof(T) == typeof(bool) && response.StatusCode == HttpStatusCode.NoContent)
                {
                    return (T)(object)true;
                }

                var content = await response.Content.ReadAsStringAsync();
                var amplifyResponse = JsonConvert.DeserializeObject<T>(content);

                return amplifyResponse;

            }
            return default;
        }
    }
}
