using CSharpFunctionalExtensions;
using Frontend.Services.Interface;
using Frontend.Services;
using Frontend.ViewModel;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly TokenProvider _tokenProvider;

    public AuthService(IHttpClientFactory httpClientFactory, TokenProvider tokenProvider)
    {
        _httpClientFactory = httpClientFactory;
        _tokenProvider = tokenProvider;
    }

    public async Task<Result<string>> GetToken(LoginViewModel request)
    {
        var client = _httpClientFactory.CreateClient("Gateway");

        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/login", content);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure<string>("Error");
        }

        var responseData = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<dynamic>(responseData);
        var token = data.token.ToString();

        _tokenProvider.StoreTokenAsCookie(token);

        return Result.Success(token);
    }
}
