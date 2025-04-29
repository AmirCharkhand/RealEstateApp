using RealEstateApp.ViewModels;

namespace RealEstateApp.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel viewModel)
	{
		BindingContext=viewModel;
		InitializeComponent();
	}
}