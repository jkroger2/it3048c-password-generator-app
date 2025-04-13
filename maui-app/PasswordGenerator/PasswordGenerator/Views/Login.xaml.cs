using PasswordGenerator.Models;
using PasswordGenerator.Models.ViewModels;

namespace PasswordGenerator.Views
{
    public partial class Login : ContentPage
    {
        private readonly LoginViewModel _viewModel;
        public Login(LoginViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await _viewModel.LoginAsync();
                
                if (!result.Success)
                {
                    await DisplayAlert("Error", result.Message, "OK");
                    return;
                }

                AppShell shell = ((App)Application.Current).Services.GetService<AppShell>();
                ((App)Application.Current).MainPage = shell;
            }
            catch (UnauthorizedAccessException ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void GoToRegisterPage(object sender, EventArgs e)
        {
            Register registerPage = ((App)Application.Current).Services.GetService<Register>();
            await Navigation.PushAsync(registerPage);
        }
    }

}

