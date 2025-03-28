using RealEstateApp.ViewModels;

namespace RealEstateApp.Views;

public partial class RegisterNewUserPage : ContentPage
{
	public RegisterNewUserPage(RegisterNewUserViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}