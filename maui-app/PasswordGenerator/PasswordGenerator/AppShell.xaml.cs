using System;
using Microsoft.Maui.Controls;
using PasswordGenerator.Views;

namespace PasswordGenerator
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Login), typeof(Login));

            Routing.RegisterRoute(nameof(Register), typeof(Register));

            Routing.RegisterRoute(nameof(Vault), typeof(Vault));

            Routing.RegisterRoute(nameof(Views.PasswordGenerator), typeof(Views.PasswordGenerator));

            Routing.RegisterRoute(nameof(AddAccount), typeof(AddAccount));

            Routing.RegisterRoute(nameof(EditAccount), typeof(EditAccount));

        }

    }
}
