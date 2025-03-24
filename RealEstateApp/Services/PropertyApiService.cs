using RealEstateApp.Models;
using System.Net.Http.Json;

namespace RealEstateApp.Services
{
    public class PropertyApiService(HttpClient httpClient, UriBuilderService uriBuilderService) : IDisposable
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly UriBuilderService _uriBuilderService = uriBuilderService;

        public async Task<IEnumerable<Property>> GetPropertiesAsync(int categoryId)
        {
            var uri = _uriBuilderService.GetPropertiesListUri(categoryId);
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var properties = await response.Content.ReadFromJsonAsync<IEnumerable<Property>>()
                ?? throw new Exception("Invalid response from server");
            return properties;
        }

        public async Task<Property> GetPropertyDetailsAsync(int propertyId)
        {
            var uri = _uriBuilderService.GetPropertyDetailsUri(propertyId);
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var property = await response.Content.ReadFromJsonAsync<Property>()
                ?? throw new Exception("Invalid response from server");
            return property;
        }

        public async Task<IEnumerable<Property>> GetTrendingPropertiesAsync()
        {
            var uri = _uriBuilderService.GetTrendingPropertiesUri();
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var properties = await response.Content.ReadFromJsonAsync<IEnumerable<Property>>()
                ?? throw new Exception("Invalid response from server");
            return properties;
        }

        public async Task<IEnumerable<Property>> SearchPropertiesAsync(string address)
        {
            var uri = _uriBuilderService.GetSearchPropertiesUri(address);
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var properties = await response.Content.ReadFromJsonAsync<IEnumerable<Property>>()
                ?? throw new Exception("Invalid response from server");
            return properties;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
