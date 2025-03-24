using RealEstateApp.Exceptions;
using RealEstateApp.Services;
using System.Net.Http.Headers;

namespace RealEstateApp.Extensions
{
    public static class AddApiConnectivityServicesExtensions
    {
        public static IServiceCollection AddApiConectivityServices(this IServiceCollection services)
        {
            services.AddHttpClient<UserApiService>();
            services.AddHttpClient<CateroryApiService>(async (sp, c) =>
            {
                var loginInfoStorage = sp.GetRequiredService<LoginInfoStorageService>();
                var loginToken = await loginInfoStorage.GetLoginInfoAsync()
                    ?? throw new UserLoginException();

                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginToken.AccessToken);
            });
            services.AddHttpClient<PropertyApiService>(async (sp, c) =>
            {
                var loginInfoStorage = sp.GetRequiredService<LoginInfoStorageService>();
                var loginToken = await loginInfoStorage.GetLoginInfoAsync()
                    ?? throw new UserLoginException();

                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginToken.AccessToken);
            });

            return services;
        }
    }
}
