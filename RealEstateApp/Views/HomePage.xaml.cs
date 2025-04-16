using RealEstateApp.ViewModels;

namespace RealEstateApp.Views;

public partial class HomePage : ContentPage
{
	private readonly HomePageViewModel _viewModel;
    public HomePage(HomePageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Task.WhenAll(
            _viewModel.SetCategoriesCommand.ExecuteAsync(null),
            _viewModel.SetUserInfoCommand.ExecuteAsync(null),
            _viewModel.SetTrendingPropertiesCommand.ExecuteAsync(null)
        );
    }
}