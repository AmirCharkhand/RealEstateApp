using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RealEstateApp.Models;
using RealEstateApp.Services;

namespace RealEstateApp.ViewModels
{
    public partial class RegisterNewUserViewModel : BaseViewModel
    {
        private readonly IServiceProvider _serviceProvider;

        private UserApiService UserApiService => _serviceProvider.GetRequiredService<UserApiService>();

        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private string _email;
        [ObservableProperty]
        private string _password;
        [ObservableProperty]
        private string _phone;

        public RegisterNewUserViewModel(IServiceProvider serviceProvider)
        {
            Title = "Sign Up";
            _serviceProvider = serviceProvider;
        }

        [RelayCommand]
        private async Task RegisterUser()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Phone))
            {
                await Shell.Current.DisplayAlert("Error", "All fields are required.", "OK");
                return;
            }
            try
            {
                IsBusy = true;
                var newUser = new NewUser(Name, Email, Password, Phone);
                await UserApiService.RegisterUserAsync(newUser);
                await Shell.Current.DisplayAlert("Success", "User registered successfully. You can login now", "OK");
                await GoToLoginPage();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Registration failed: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task GoToLoginPage()
        {
            await Shell.Current.GoToAsync("..", true);
        }
    }
}