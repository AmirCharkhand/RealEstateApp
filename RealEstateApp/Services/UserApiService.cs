using RealEstateApp.Models;
using System.Net.Http.Json;

namespace RealEstateApp.Services
{
    public class UserApiService(HttpClient httpClient, UriBuilderService uriBuilderService) : IDisposable
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly UriBuilderService _uriBuilder = uriBuilderService;

        public async Task RegisterUserAsync(NewUser user)
        {
            var uri = _uriBuilder.GetRegisterUserUri();
            var response = await _httpClient.PostAsJsonAsync<NewUser>(uri, user);
            response.EnsureSuccessStatusCode();
        }

        public async Task<LoginToken> LoginUserAsync(UserLogin userLogin)
        {
            var uri = _uriBuilder.GetLoginUserUri();
            var response = await _httpClient.PostAsJsonAsync(uri, userLogin);
            response.EnsureSuccessStatusCode();
            var loginToken = await response.Content.ReadFromJsonAsync<LoginToken>() 
                ?? throw new Exception("Invalid response from server");

            return loginToken;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}