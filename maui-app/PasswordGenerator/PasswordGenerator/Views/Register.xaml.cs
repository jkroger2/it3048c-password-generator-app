using PasswordGenerator.Models;
using PasswordGenerator.Models.ViewModels;

namespace PasswordGenerator.Views
{
    public partial class Register : ContentPage
    {
        private readonly RegisterViewModel _viewModel;
        public Register(RegisterViewModel viewmodel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewmodel;
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await _viewModel.RegisterAsync();

                if (!result.Success)
                {
                    await DisplayAlert("Error", result.Message, "OK");
                    return;
                }

                Vault vaultPage = ((App)Application.Current).Services.GetService<Vault>();
                await Navigation.PushAsync(vaultPage);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void GoToLoginPage(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}


