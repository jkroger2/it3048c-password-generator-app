using Microsoft.Maui.Controls;

using PasswordGenerator.Models;

namespace PasswordGenerator.Pages;

public partial class VaultPage : ContentPage
{
    User user;
    public VaultPage(User user)
    {
        InitializeComponent();
        this.user = user;
    }

    private async void OnAccountTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditItemPage());
    }


    private async void OnAddPasswordClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync( new AddPasswordPage());
    }

    private void OnDarkModeClicked(object sender, EventArgs e)
    {
        var current = Application.Current.UserAppTheme;
        Application.Current.UserAppTheme = current == AppTheme.Dark ? AppTheme.Light : AppTheme.Dark;
    }

}
