using RealEstateApp.Models;
using RealEstateApp.Services;

namespace RealEstateApp.Extensions
{
    public static class AddUtillitiesAndHelpersExtensions
    {
        public static IServiceCollection AddUriBuilderService(this IServiceCollection services)
        {
            return services
                .AddSingleton<UriBuilderService>()
                .AddSingleton<LoginInfoStorageService>()
                .AddScoped<SqliteService<BookmarkedProperty>>();
        }
    }
}
