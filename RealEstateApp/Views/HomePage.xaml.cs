using RealEstateApp.ViewModels;

namespace RealEstateApp.Views;

public partial class HomePage : ContentPage
{
    public HomePage(HomePageViewModel viewModel)
	{
        BindingContext = viewModel;
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is HomePageViewModel viewModel)
        {
            await viewModel.InitializeAsync();
        }
        else
        {
            throw new InvalidOperationException($"BindingContext is not of type {typeof(HomePageViewModel)}");
        }
    }
}