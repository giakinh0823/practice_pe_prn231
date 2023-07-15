using Newtonsoft.Json;

namespace Client.Helper
{
    public class ApiHelper
    {
        internal readonly HttpClient _httpClient;

        public ApiHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TResponse?> GetAsync<TResponse>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(content);
        }

        public async Task<TResponse?> PostAsync<TResponse>(string url, IDictionary<string, string> data)
        {
            var content = new FormUrlEncodedContent(data);
            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }

        public async Task<TResponse?> PutAsync<TResponse>(string url, IDictionary<string, string> data)
        {
            var content = new FormUrlEncodedContent(data);
            var response = await _httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }

        public async Task<TResponse?> DeleteAsync<TResponse>(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(content);
        }

        public async Task<TResponse?> SearchAsync<TResponse>(string url, IDictionary<string, string> parameters)
        {
            var queryString = new FormUrlEncodedContent(parameters).ReadAsStringAsync().Result;
            var fullUrl = $"{url}?{queryString}";

            var response = await _httpClient.GetAsync(fullUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(content);
        }
    }
}
