using Newtonsoft.Json;
using System.Net.Http;

namespace Client.Helper
{
    public class ApiOdataHelper : ApiHelper
    {
        public ApiOdataHelper(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<TResponse?> GetWithODataAsync<TResponse>(string url, IDictionary<string, string> parameters)
        {
            var queryString = BuildODataQueryString(parameters);
            var fullUrl = $"{url}?{queryString}";

            var response = await _httpClient.GetAsync(fullUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(content);
        }

        public async Task<TResponse?> GetOneWithODataAsync<TResponse>(string url, string id, IDictionary<string, string> parameters)
        {
            var queryString = BuildODataQueryString(parameters);
            var fullUrl = $"{url}/{id}?{queryString}";

            var response = await _httpClient.GetAsync(fullUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(content);
        }

        public async Task<TResponse?> SearchWithODataAsync<TResponse>(string url, string searchTerm, IDictionary<string, string> parameters)
        {
            var encodedSearchTerm = Uri.EscapeDataString(searchTerm);
            parameters["$search"] = encodedSearchTerm;

            var queryString = BuildODataQueryString(parameters);
            var fullUrl = $"{url}?{queryString}";

            var response = await _httpClient.GetAsync(fullUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(content);
        }

        public async Task<TResponse?> PostWithODataAsync<TResponse>(string url, IDictionary<string, string> data)
        {
            var content = new FormUrlEncodedContent(data);
            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }

        public async Task<TResponse?> PutWithODataAsync<TResponse>(string url, string id, IDictionary<string, string> data)
        {
            var content = new FormUrlEncodedContent(data);
            var response = await _httpClient.PutAsync($"{url}/{id}", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }

        public async Task<TResponse?> DeleteWithODataAsync<TResponse>(string url, string id)
        {
            var response = await _httpClient.DeleteAsync($"{url}/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(content);
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
