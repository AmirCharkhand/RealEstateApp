using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RealEstateApp.Models;
using RealEstateApp.Services;
using RealEstateApp.Views;
using System.Collections.ObjectModel;

namespace RealEstateApp.ViewModels
{
    public partial class BookmarksViewModel : BaseViewModel
    {
        private readonly SqliteService<BookmarkedProperty> _sqliteService;

        public ObservableCollection<BookmarkedProperty> BookmarkedProperties { get; } = new ObservableCollection<BookmarkedProperty>();
        [ObservableProperty]
        private BookmarkedProperty? _selectedProperty;

        public BookmarksViewModel(SqliteService<BookmarkedProperty> sqliteService) 
        {
            _sqliteService = sqliteService;
            Title = "Bookmarked Properties";
        }

        public async Task LoadBookmarks()
        {
            IsBusy = true;
            try
            {
                var bookmarks = await _sqliteService.GetAllItemsAsync();
                BookmarkedProperties.Clear();
                foreach (var bookmark in bookmarks)
                    BookmarkedProperties.Add(bookmark);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load bookmarks: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task GoToPropertyDetailsPage()
        {
            if (SelectedProperty == null) 
                return;
            await Shell.Current.GoToAsync($"{nameof(PropertyDetailsPage)}?{nameof(PropertyDetailsViewModel.PropertyId)}={SelectedProperty.Id}");
            SelectedProperty = null;
        }
    }
}
