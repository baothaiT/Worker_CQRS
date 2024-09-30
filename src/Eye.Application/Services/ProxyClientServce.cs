using Eye.Application.Services.Interface;
using Eye.Contract.Share.DTO;
using Eye.Contract.Share.Enum;
using Eye.Contract.Share.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Eye.Application.Services
{
    public class ProxyClientServce: IProxyClientServce
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string BaseUrl;
        private readonly string ApiKey;
        private readonly string Componemt;

        public ProxyClientServce(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            BaseUrl = "http://localhost:5199";
            ApiKey = String.Empty;
            Componemt = "Proxy";
        }

        public async Task<IEnumerable<GetProxyDto>> AllProxy()
        {
            Console.WriteLine("Start - Get All Proxies");
            var requestUrl = $"{BaseUrl}/{Componemt}";
            var client = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await client.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode(); // Throws if status code is not 2xx

            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine("End - Get All Proxies");
            return JsonSerializer.Deserialize<IEnumerable<GetProxyDto>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<GetProxyDto>> IsProxyWorking(IEnumerable<GetProxyDto> proxies)
        {
            Console.WriteLine("Start - Check Proxies is working");
            var requestUrl = $"{BaseUrl}/{Componemt}/CheckProxies";
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(proxies), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(requestUrl, content);
            response.EnsureSuccessStatusCode(); // Throws if status code is not 2xx
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine("End - Check Proxies is working");
            return JsonSerializer.Deserialize<IEnumerable<GetProxyDto>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateAllProxies(List<GetProxyDto> proxies)
        {
            Console.WriteLine("Start - Update All Proxies");
            var requestUrl = $"{BaseUrl}/{Componemt}/UpdateProxies";
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(proxies), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(requestUrl, content);
            response.EnsureSuccessStatusCode(); // Throws if status code is not 2xx
            Console.WriteLine("End - Update All Proxies");
        }

        public async Task<IEnumerable<GetProxyDto>> GetAllProxiesByStatus(ProxyStatusEnum proxyStatus)
        {
            Console.WriteLine("Start - Get All Proxies By Status" + nameof(proxyStatus));
            var requestUrl = $"{BaseUrl}/{Componemt}/GetProxiesByStatus/{proxyStatus}";
            var client = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await client.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode(); // Throws if status code is not 2xx

            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine("End - Get All Proxies");
            return JsonSerializer.Deserialize<IEnumerable<GetProxyDto>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

    }
}
