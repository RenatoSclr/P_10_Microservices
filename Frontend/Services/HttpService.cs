using CSharpFunctionalExtensions;
using Frontend.Services.Interface;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Frontend.Services
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result<T>> GetAsync<T>(string url, string token)
        {
            var client = await GetAuthorizedClient(token);
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return Result.Failure<T>($"Erreur lors de l'appel GET : {response.StatusCode}");

            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        public async Task<Result> PostAsync<T>(string url, T data, string token)
        {
            var client = await GetAuthorizedClient(token);
            var content = SerializeToHttpContent(data);

            var response = await client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
                return Result.Failure($"Erreur lors de l'appel POST : {response.StatusCode}");

            return Result.Success();
        }

        public async Task<Result> PutAsync<T>(string url, T data, string token)
        {
            var client = await GetAuthorizedClient(token);
            var content = SerializeToHttpContent(data);

            var response = await client.PutAsync(url, content);

            if (!response.IsSuccessStatusCode)
                return Result.Failure($"Erreur lors de l'appel PUT : {response.StatusCode}");

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(string url, string token)
        {
            var client = await GetAuthorizedClient(token);

            var response = await client.DeleteAsync(url);

            if (!response.IsSuccessStatusCode)
                return Result.Failure($"Erreur lors de l'appel DELETE : {response.StatusCode}");

            return Result.Success();
        }

        private HttpContent SerializeToHttpContent<T>(T data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            return new StringContent(jsonData, Encoding.UTF8, "application/json");
        }

        private async Task<HttpClient> GetAuthorizedClient(string token)
        {
            var client = _httpClientFactory.CreateClient("Gateway");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }
    }
}
