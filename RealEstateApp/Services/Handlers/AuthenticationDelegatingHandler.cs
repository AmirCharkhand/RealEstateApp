using RealEstateApp.Exceptions;
using System.Net.Http.Headers;

namespace RealEstateApp.Services.Handlers
{
    public class AuthenticationDelegatingHandler (LoginInfoStorageService loginInfoStorageService) : DelegatingHandler
    {
        private readonly LoginInfoStorageService _loginInfoStorageService = loginInfoStorageService;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var loginToken = await _loginInfoStorageService.GetLoginInfoAsync()
                ?? throw new UserLoginException();
            
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginToken.AccessToken);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}