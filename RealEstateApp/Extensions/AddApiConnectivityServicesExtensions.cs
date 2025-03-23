using RealEstateApp.Services;

namespace RealEstateApp.Extensions
{
    public static class AddApiConnectivityServicesExtensions
    {
        public static IServiceCollection AddApiConectivityServices(this IServiceCollection services)
        {
            services.AddHttpClient<UserApiService>();

            return services;
        }
    }
}
