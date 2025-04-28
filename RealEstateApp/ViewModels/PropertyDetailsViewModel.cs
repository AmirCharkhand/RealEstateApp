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
        private SqliteService<BookmarkedProperty> BookmarkedPropertySqlite => _serviceProvider.GetRequiredService<SqliteService<BookmarkedProperty>>();

        [ObservableProperty]
        private int _propertyId;
        [ObservableProperty]
        private Property? _property;
        [ObservableProperty]
        private string? _bookmarkIcon;

        public async Task InitializeAsync()
        {
            if (PropertyId == 0)
            {
                await Shell.Current.DisplayAlert("Error", "Property ID is not set.", "OK");
                await GoBack();
                return;
            }
            await Task.WhenAll(
                GetPropertyDetails(),
                SetBookmarkIcon());
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

        private async Task SetBookmarkIcon()
        {
            var property = await BookmarkedPropertySqlite.GetItemAsync(PropertyId);
            BookmarkIcon = property != null ? "bookmark_fill_icon" : "bookmark_empty_icon";
        }

        [RelayCommand]
        private async Task ToggleBookmark()
        {
            if (Property == null)
                throw new ArgumentNullException(nameof(Property));

            var property = await BookmarkedPropertySqlite.GetItemAsync(PropertyId);
            if (property != null)
                await BookmarkedPropertySqlite.DeleteItemAsync(property);
            else
            {
                var newProperty = new BookmarkedProperty
                {
                    Id = PropertyId,
                    Name = Property.Name,
                    Address = Property.Address,
                    ImageUrl = Property.ImageUrl,
                    Price = Property.Price,
                };
                await BookmarkedPropertySqlite.SaveItemAsync(newProperty);
            }

            await SetBookmarkIcon();
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
