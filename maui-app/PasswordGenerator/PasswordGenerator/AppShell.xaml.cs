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

            Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));

            Routing.RegisterRoute(nameof(RegisterView), typeof(RegisterView));

            Routing.RegisterRoute(nameof(VaultView), typeof(VaultView));

            Routing.RegisterRoute(nameof(PasswordGeneratorView), typeof(PasswordGeneratorView));

            Routing.RegisterRoute(nameof(AddAccountView), typeof(AddAccountView));

            Routing.RegisterRoute(nameof(EditAccountView), typeof(EditAccountView));

        }
    }
}
