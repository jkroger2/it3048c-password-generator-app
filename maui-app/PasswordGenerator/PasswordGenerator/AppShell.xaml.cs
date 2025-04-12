using System;
using Microsoft.Maui.Controls;
using PasswordGenerator.Pages;

namespace PasswordGenerator
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing .RegisterRoute(nameof(VaultPage), typeof(VaultPage));

            Routing.RegisterRoute(nameof(EditItemPage), typeof(EditItemPage));

            Routing.RegisterRoute(nameof(AddPasswordPage), typeof(AddPasswordPage));

        }

    }
}
