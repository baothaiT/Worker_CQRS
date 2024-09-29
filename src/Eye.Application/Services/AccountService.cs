using Eye.Contract.Share.DTO._JoinDTO;
using Eye.Application.Services.Interface;
using Eye.Contract.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Eye.Contract.Share.DTO.Account;

namespace Eye.Application.Services;

public class AccountService: IAccountService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string BaseUrl = "http://localhost:5199";
    private const string ApiKey = ""; // Replace with your API key
    private const string Componemt = "Account";

    public AccountService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<Account_Browser_DTO>> Account_Browser_DTO(string AccountId)
    {
        var requestUrl = $"{BaseUrl}/{Componemt}/GetBrowserByAccountId/{AccountId}";
        var client = _httpClientFactory.CreateClient();

        // Send a GET request to the API
        HttpResponseMessage response = await client.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Account_Browser_DTO>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<List<GetAccountsDto>> GetAl()
    {
        var requestUrl = $"{BaseUrl}/{Componemt}";
        var client = _httpClientFactory.CreateClient();

        // Send a GET request to the API
        HttpResponseMessage response = await client.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<GetAccountsDto>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    }
}
