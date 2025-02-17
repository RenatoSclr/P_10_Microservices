﻿using CSharpFunctionalExtensions;
using Frontend.Services.Interface;
using Frontend.ViewModel;
using Newtonsoft.Json;
using System.Text;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenProvider _tokenProvider;

    public AuthService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
    {
        _httpClientFactory = httpClientFactory;
        _tokenProvider = tokenProvider;
    }

    public async Task<Result> SignInAsync(LoginViewModel request)
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

        return Result.Success();
    }
}
