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

            var loginPage = Services.GetService<Login>();
            MainPage = new NavigationPage(loginPage);
        }
    }
}
