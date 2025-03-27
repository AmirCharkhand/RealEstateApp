namespace RealEstateApp.Extensions
{
    public static class AddApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services
                .AddUriBuilderService()
                .AddApiConectivityServices()
                .AddAppPages();
        }
    }
}
