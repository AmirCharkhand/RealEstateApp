using RealEstateApp.ViewModels;

namespace RealEstateApp.Views;

public partial class PropertiesListPage : ContentPage
{    
    public PropertiesListPage(PropertiesListViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        if (BindingContext is PropertiesListViewModel viewModel)
        {
            await viewModel.InitializeAsync();
        }
        else
        {
            throw new InvalidOperationException($"BindingContext is not of type {typeof(PropertiesListViewModel)}");
        }
    }
}