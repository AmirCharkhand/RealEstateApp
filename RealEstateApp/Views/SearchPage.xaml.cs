using RealEstateApp.ViewModels;

namespace RealEstateApp.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage(SearchViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}
}