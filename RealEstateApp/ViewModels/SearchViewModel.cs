using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RealEstateApp.Exceptions;
using RealEstateApp.Models;
using RealEstateApp.Services;
using RealEstateApp.Views;
using System.Collections.ObjectModel;

namespace RealEstateApp.ViewModels
{
    public partial class SearchViewModel(IServiceProvider serviceProvider) : BaseViewModel
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private PropertyApiService PropertyApiService => _serviceProvider.GetRequiredService<PropertyApiService>();
        [ObservableProperty]
        private string? _searchText;
        [ObservableProperty]
        private Property? _selectedProperty;
        public ObservableCollection<Property> SearchResults { get; } = new ObservableCollection<Property>();

        [RelayCommand]
        private async Task Search()
        {
            if (string.IsNullOrEmpty(SearchText))
                return;

            SearchResults.Clear();
            try
            {
                IsBusy = true;
                var properties = await PropertyApiService.SearchPropertiesAsync(SearchText);
                foreach (var property in properties)
                    SearchResults.Add(property);
            }
            catch (Exception ex) when (ex is not UserLoginException)
            {
                // Handle other exceptions, such as network errors or server errors
                await Shell.Current.DisplayAlert("Error", $"Couldn't retrieve search results from server {ex.Message}", "OK");
            }
            catch (UserLoginException ex)
            {
                await Shell.Current.DisplayAlert("Error", $"User is not logged in. Please Login first {ex.Message}", "OK");
                await Shell.Current.GoToAsync("//LoginPage");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task GotoPropertyDetails()
        {
            if (SelectedProperty == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(PropertyDetailsPage)}?{nameof(PropertyDetailsViewModel.PropertyId)}={SelectedProperty.Id}");
            SelectedProperty=null;
        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}