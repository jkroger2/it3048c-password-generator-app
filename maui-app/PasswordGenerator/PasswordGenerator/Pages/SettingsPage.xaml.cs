using System;
using Microsoft.Maui.Controls;

namespace PasswordGenerator.Pages
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void OnAccountTapped(object sender, EventArgs e)
        {
            AccountDetails.IsVisible = !AccountDetails.IsVisible;
        }

        private void OnPasswordTapped(object sender, EventArgs e)
        {
            PasswordDetails.IsVisible = !PasswordDetails.IsVisible;
        }

        private void OnAboutTapped(object sender, EventArgs e)
        {
            AboutDetails.IsVisible = !AboutDetails.IsVisible;
        }

        private void OnDarkModeClicked(object sender, EventArgs e)
        {
            var current = Application.Current.UserAppTheme;
            Application.Current.UserAppTheme = current == AppTheme.Dark ? AppTheme.Light : AppTheme.Dark;
        }

    }
}


