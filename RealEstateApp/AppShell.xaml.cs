using RealEstateApp.Services;
using RealEstateApp.Views;

namespace RealEstateApp
{
    public partial class AppShell : Shell
    {
        private readonly LoginInfoStorageService _loginInfoStorageService;
        public AppShell(LoginInfoStorageService loginInfoStorageService)
        {
            _loginInfoStorageService = loginInfoStorageService;
            InitializeComponent();
            Routing.RegisterRoute(nameof(RegisterNewUserPage), typeof(RegisterNewUserPage));
            Routing.RegisterRoute(nameof(PropertiesListPage), typeof(PropertiesListPage));
            Routing.RegisterRoute(nameof(PropertyDetailsPage), typeof(PropertyDetailsPage));
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
        }

        protected async override void OnAppearing()
        {
            var isLoginTokenValid = await _loginInfoStorageService.IsLoginTokenValid();
            if (isLoginTokenValid)
                await Shell.Current.GoToAsync("//HomePage");
            else
                await Shell.Current.GoToAsync("//LoginPage");

            base.OnAppearing();
        }
    }
}
