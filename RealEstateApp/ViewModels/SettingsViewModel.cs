
using CommunityToolkit.Mvvm.Input;
using RealEstateApp.Services;

namespace RealEstateApp.ViewModels
{
    public partial class SettingsViewModel : BaseViewModel
    {
        private readonly LoginInfoStorageService _loginInfoStorageService;
        public SettingsViewModel(LoginInfoStorageService loginInfoStorage)
        {
            _loginInfoStorageService = loginInfoStorage;
            Title = "Settings";
        }

        [RelayCommand]
        private async Task Logout()
        {
            _loginInfoStorageService.ClearLoginInfo();
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
