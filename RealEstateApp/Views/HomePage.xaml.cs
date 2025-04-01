using RealEstateApp.ViewModels;

namespace RealEstateApp.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}