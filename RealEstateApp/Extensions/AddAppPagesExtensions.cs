using RealEstateApp.ViewModels;
using RealEstateApp.Views;

namespace RealEstateApp.Extensions
{
    public static class AddAppPagesExtensions
    {
        public static IServiceCollection AddAppPages(this IServiceCollection services)
        {
            return services
                .AddTransient<LoginPage>()
                .AddTransient<LoginViewModel>()
                .AddTransient<RegisterNewUserPage>()
                .AddTransient<RegisterNewUserViewModel>()
                .AddSingleton<HomePage>()
                .AddSingleton<HomePageViewModel>()
                .AddTransient<PropertiesListPage>()
                .AddTransient<PropertiesListViewModel>()
                .AddTransient<PropertyDetailsPage>()
                .AddTransient<PropertyDetailsViewModel>()
                .AddTransient<SearchPage>()
                .AddTransient<SearchViewModel>()
                .AddSingleton<BookmarksPage>()
                .AddSingleton<SettingsPage>();
        }
    }
}
