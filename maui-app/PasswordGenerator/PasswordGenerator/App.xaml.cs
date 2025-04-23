using PasswordGenerator.Services;
using PasswordGenerator.Views;

namespace PasswordGenerator
{
    public partial class App : Application
    {
        public IServiceProvider Services { get; }
        public App(IServiceProvider services)
        {
            InitializeComponent();
            Services = services;

            var loginView = Services.GetService<LoginView>();
            MainPage = new NavigationPage(loginView);
        }
    }
}
