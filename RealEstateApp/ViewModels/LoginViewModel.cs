using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RealEstateApp.Models;
using RealEstateApp.Services;
using RealEstateApp.Views;

namespace RealEstateApp.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly LoginInfoStorageService _loginInfoStorageService;

        [ObservableProperty]
        private string _email = string.Empty;
        [ObservableProperty]
        private string _password = string.Empty;

        private UserApiService UserApiService => _serviceProvider.GetRequiredService<UserApiService>();

        public LoginViewModel(IServiceProvider serviceProvider, LoginInfoStorageService loginInfoStorageService)
        {
            Title = "Login";
            _serviceProvider = serviceProvider;
            _loginInfoStorageService = loginInfoStorageService;
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            IsBusy = true;
            try
            {
                var loginToken = await UserApiService.LoginUserAsync(new UserLogin (Email, Password));
                // Save the login token to secure storage
                await _loginInfoStorageService.SaveLoginInfoAsync(loginToken);
                // Navigate to the home page
                await Shell.Current.GoToAsync("//HomePage");
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error", $"Invalid email or password: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task GotoRegisterPage()
        {
            await Shell.Current.GoToAsync(nameof(RegisterNewUserPage));
        }
    }
}
