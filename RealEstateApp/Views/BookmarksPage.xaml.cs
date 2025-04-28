using RealEstateApp.ViewModels;

namespace RealEstateApp.Views;

public partial class BookmarksPage : ContentPage
{
	public BookmarksPage(BookmarksViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is BookmarksViewModel viewModel)
            await viewModel.LoadBookmarks();
        else
            throw new Exception("ViewModel is not set or is not of type BookmarksViewModel.");
    }
}