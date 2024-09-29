using Eye.Application.Services.Interface;
using Eye.Contract.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Eye.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string BaseUrl;
        private readonly string ApiKey;
        private readonly string Componemt;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            BaseUrl = "http://localhost:5199";
            ApiKey = String.Empty;
            Componemt = "Account";
        }

        public async Task<IEnumerable<GetProductDTO>> GetProductsAsync()
        {
            var requestUrl = $"{BaseUrl}/{Componemt}";
            var client = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await client.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode(); // Throws if status code is not 2xx

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<GetProductDTO>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        //public async Task<GetProductDTO> GetProductByIdAsync(string id)
        //{
        //    var response = await _httpClient.GetAsync($"ProductsMediatR/{id}");
        //    response.EnsureSuccessStatusCode();

        //    var jsonResponse = await response.Content.ReadAsStringAsync();
        //    return JsonSerializer.Deserialize<GetProductDTO>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //}

        //public async Task CreateProductAsync(CreateProductDto product)
        //{
        //    var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PostAsync("ProductsMediatR", content);
        //    response.EnsureSuccessStatusCode();
        //}

        //public async Task UpdateProductAsync(UpdateProductDto product)
        //{
        //    var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PutAsync($"ProductsMediatR/{product.Id}", content);
        //    response.EnsureSuccessStatusCode();
        //}

        //public async Task DeleteProductAsync(string id)
        //{
        //    var response = await _httpClient.DeleteAsync($"ProductsMediatR/{id}");
        //    response.EnsureSuccessStatusCode();
        //}
    }
}
