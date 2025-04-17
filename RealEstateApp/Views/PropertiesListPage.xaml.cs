using RealEstateApp.ViewModels;

namespace RealEstateApp.Views;

public partial class PropertiesListPage : ContentPage
{    
    public PropertiesListPage(PropertiesListViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}
}