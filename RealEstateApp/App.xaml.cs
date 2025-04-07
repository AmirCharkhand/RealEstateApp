using RealEstateApp.Services;

namespace RealEstateApp
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell(_serviceProvider.GetRequiredService<LoginInfoStorageService>()));
        }
    }
}