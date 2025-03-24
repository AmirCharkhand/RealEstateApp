using RealEstateApp.Models;
using System.Net.Http.Json;

namespace RealEstateApp.Services
{
    public class CateroryApiService (HttpClient httpClient, UriBuilderService uriBuilderService) : IDisposable
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly UriBuilderService _uriBuilder = uriBuilderService;

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var uri = _uriBuilder.CategoriesAddress;
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var categories = await response.Content.ReadFromJsonAsync<IEnumerable<Category>>()
                ?? throw new Exception("Invalid response from server");
            return categories;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
