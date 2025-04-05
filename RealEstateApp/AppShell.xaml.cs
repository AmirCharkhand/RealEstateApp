using RealEstateApp.Views;

namespace RealEstateApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RegisterNewUserPage), typeof(RegisterNewUserPage));
        }
    }
}
