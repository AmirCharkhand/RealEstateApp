using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RealEstateApp.Exceptions;
using RealEstateApp.Models;
using RealEstateApp.Services;
using RealEstateApp.Views;
using System.Collections.ObjectModel;

namespace RealEstateApp.ViewModels
{
    public partial class HomePageViewModel(LoginInfoStorageService loginInfoStorageService, IServiceProvider serviceProvider) : BaseViewModel
    {
        private readonly LoginInfoStorageService _loginInfoStorageService = loginInfoStorageService;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        private CategoryApiService CategoryApiService => _serviceProvider.GetRequiredService<CategoryApiService>();
        private PropertyApiService PropertyApiService => _serviceProvider.GetRequiredService<PropertyApiService>();

        [ObservableProperty]
        private string _username;
        [ObservableProperty]
        private Category _selectedCategory;

        public ObservableCollection<Category> Categories { get; init; } = new ObservableCollection<Category>();
        public ObservableCollection<Property> TrendingProperties { get; init; } = new ObservableCollection<Property>();

        public async Task InitializeAsync()
        {
            await Task.WhenAll(
                SetUserInfo(),
                SetCategories(),
                SetTrendingProperties());
        }

        private async Task SetUserInfo()
        {
            var loginInfo = await _loginInfoStorageService.GetLoginInfoAsync();
            if (loginInfo != null)
                Username = $"Welcome {loginInfo.UserName}";
            else
            {
                await Shell.Current.DisplayAlert("Error", "User not found! You need to Login first", "OK");
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }

        private async Task SetCategories()
        {
            if (Categories.Count > 0)
                return;

            try
            {
                IsBusy = true;
                var categories = await CategoryApiService.GetCategoriesAsync();
                foreach (var category in categories)
                    Categories.Add(category);
            }
            catch (Exception ex) when (ex is not UserLoginException)
            {
                // Handle other exceptions, such as network errors or server errors
                await Shell.Current.DisplayAlert("Error", $"Couldn't retrieve Categories from server {ex.Message}", "OK");
            }
            catch (UserLoginException ex)
            {
                await Shell.Current.DisplayAlert("Error", $"User is not loged in. Please Login first {ex.Message}", "OK");
                await Shell.Current.GoToAsync("//LoginPage");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task SetTrendingProperties()
        {
            if (TrendingProperties.Count > 0)
                return;

            try
            {
                IsBusy = true;
                var trendingProperties = await PropertyApiService.GetTrendingPropertiesAsync();
                foreach (var property in trendingProperties)
                    TrendingProperties.Add(property);
            }
            catch (Exception ex) when (ex is not UserLoginException)
            {
                // Handle other exceptions, such as network errors or server errors
                await Shell.Current.DisplayAlert("Error", $"Couldn't retrieve Trending Properties from server {ex.Message}", "OK");
            }
            catch (UserLoginException ex)
            {
                await Shell.Current.DisplayAlert("Error", $"User is not loged in. Please Login first {ex.Message}", "OK");
                await Shell.Current.GoToAsync("//LoginPage");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task GoToPropertiesListPage()
        {
            if (SelectedCategory == null)
                return;

            var queryParameters = new Dictionary<string, object>
            {
                { nameof(Category), SelectedCategory }
            };

            await Shell.Current.GoToAsync(nameof(PropertiesListPage), true, queryParameters);
        }
    }
}