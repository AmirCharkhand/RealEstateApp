using RealEstateApp.Services;
using RealEstateApp.Services.Handlers;

namespace RealEstateApp.Extensions
{
    public static class AddApiConnectivityServicesExtensions
    {
        public static IServiceCollection AddApiConectivityServices(this IServiceCollection services)
        {
            services.AddTransient<AuthenticationDelegatingHandler>();

            services.AddHttpClient<UserApiService>();

            services
                .AddHttpClient<CategoryApiService>()
                .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

            services
                .AddHttpClient<PropertyApiService>()
                .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

            return services;
        }
    }
}