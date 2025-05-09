﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RealEstateApp.Exceptions;
using RealEstateApp.Models;
using RealEstateApp.Services;
using RealEstateApp.Views;
using System.Collections.ObjectModel;

namespace RealEstateApp.ViewModels
{
    public partial class PropertiesListViewModel(IServiceProvider serviceProvider) : BaseViewModel, IQueryAttributable
    {
        private Category? _category;
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private PropertyApiService PropertyApiService => _serviceProvider.GetRequiredService<PropertyApiService>();

        [ObservableProperty]
        private Property? _selectedProperty;

        public ObservableCollection<Property> Properties { get; init; } = new ObservableCollection<Property>();

        public void ApplyQueryAttributes(IDictionary<string, object> query) => _category = query[nameof(Category)] as Category;

        public async Task InitializeAsync()
        {
            if (_category == null)
                throw new ArgumentNullException(nameof(Category), "Category cannot be null");
            this.Title = $"Properties in {_category.Name} category";
            await SetPropertiesByCategoryId(_category.Id);
        }

        private async Task SetPropertiesByCategoryId(int categoryId)
        {
            if (Properties.Count >0)
                return;

            try
            {
                IsBusy = true;
                var properties = await PropertyApiService.GetPropertiesAsync(categoryId);
                foreach (var property in properties)
                    Properties.Add(property);
            }
            catch (Exception ex) when (ex is not UserLoginException)
            {
                // Handle other exceptions, such as network errors or server errors
                await Shell.Current.DisplayAlert("Error", $"Couldn't retrieve Properties from server {ex.Message}", "OK");
            }
            catch (UserLoginException ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                await Shell.Current.GoToAsync("//LoginPage");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task GoToPropertyDetails()
        {
            if (SelectedProperty == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(PropertyDetailsPage)}?{nameof(PropertyDetailsViewModel.PropertyId)}={SelectedProperty.Id}");
            SelectedProperty = null;
        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
