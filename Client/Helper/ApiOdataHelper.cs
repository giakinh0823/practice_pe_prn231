﻿using System.Net.Http;
using System.Text.Json;

namespace Client.Helper
{
    public class ApiOdataHelper : ApiHelper
    {
        public ApiOdataHelper(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<TResponse?> GetWithODataAsync<TResponse>(string url, IDictionary<string, string> parameters)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };


            var queryString = BuildODataQueryString(parameters);
            var fullUrl = $"{url}?{queryString}";

            var response = await _httpClient.GetAsync(fullUrl);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TResponse>(stream, options);
        }

        public async Task<TResponse?> GetOneWithODataAsync<TResponse>(string url, string id, IDictionary<string, string> parameters)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var queryString = BuildODataQueryString(parameters);
            var fullUrl = $"{url}/{id}?{queryString}";

            var response = await _httpClient.GetAsync(fullUrl);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TResponse>(stream, options);
        }

        public async Task<TResponse?> SearchWithODataAsync<TResponse>(string url, string searchTerm, IDictionary<string, string> parameters)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var encodedSearchTerm = Uri.EscapeDataString(searchTerm);
            parameters["$search"] = encodedSearchTerm;

            var queryString = BuildODataQueryString(parameters);
            var fullUrl = $"{url}?{queryString}";

            var response = await _httpClient.GetAsync(fullUrl);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TResponse>(stream, options);
        }

        public async Task<TResponse?> PostWithODataAsync<TResponse>(string url, IDictionary<string, string> data)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var content = new FormUrlEncodedContent(data);
            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TResponse>(stream, options);
        }

        public async Task<TResponse?> PutWithODataAsync<TResponse>(string url, string id, IDictionary<string, string> data)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var content = new FormUrlEncodedContent(data);
            var response = await _httpClient.PutAsync($"{url}/{id}", content);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TResponse>(stream, options);
        }

        public async Task<TResponse?> DeleteWithODataAsync<TResponse>(string url, string id)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var response = await _httpClient.DeleteAsync($"{url}/{id}");
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TResponse>(stream, options);
        }


        private string BuildODataQueryString(IDictionary<string, string> parameters)
        {
            var queryStringParams = new List<string>();
            foreach (var parameter in parameters)
            {
                var encodedKey = Uri.EscapeDataString(parameter.Key);
                var encodedValue = Uri.EscapeDataString(parameter.Value);
                var queryStringParam = $"{encodedKey}={encodedValue}";
                queryStringParams.Add(queryStringParam);
            }

            return string.Join("&", queryStringParams);
        }
    }
}
