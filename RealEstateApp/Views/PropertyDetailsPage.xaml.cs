using RealEstateApp.ViewModels;

namespace RealEstateApp.Views;

public partial class PropertyDetailsPage : ContentPage
{
	public PropertyDetailsPage(PropertyDetailsViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is PropertyDetailsViewModel viewModel)
            await viewModel.InitializeAsync();
        else
            throw new InvalidOperationException($"BindingContext is not of type {typeof(PropertyDetailsViewModel)}");
    }
}