using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RealEstateApp.Models;
using RealEstateApp.Services;

namespace RealEstateApp.ViewModels
{
    [QueryProperty(nameof(PropertyId), nameof(PropertyId))]
    public partial class PropertyDetailsViewModel(IServiceProvider serviceProvider) : BaseViewModel
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private PropertyApiService PropertiApiService => _serviceProvider.GetRequiredService<PropertyApiService>();

        [ObservableProperty]
        private int _propertyId;
        [ObservableProperty]
        private Property? _property;

        public async Task InitializeAsync()
        {
            if (PropertyId == 0)
            {
                await Shell.Current.DisplayAlert("Error", "Property ID is not set.", "OK");
                await GoBack();
                return;
            }
            await GetPropertyDetails();
        }

        private async Task GetPropertyDetails()
        {
            try
            {
                IsBusy = true;
                Property = await PropertiApiService.GetPropertyDetailsAsync(PropertyId);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Couldn't Show property details: {ex.Message}", "OK");
                await GoBack();
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task Call()
        {
            if (Property == null)
                throw new ArgumentNullException(nameof(Property));
            PhoneDialer.Open(Property.Phone);
            await Task.CompletedTask;
        }

        [RelayCommand]
        private async Task SendMessage()
        {
            if (Property == null) 
                throw new ArgumentNullException(nameof(Property));
            var message = new SmsMessage(
                $"Hi! I'm interested in your {Property.Name} property in {Property.Address}. Is it yet aveailable?",
                Property.Phone);
            await Sms.ComposeAsync(message);
        }
    }
}
