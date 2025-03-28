using System;
using Microsoft.Maui.Controls;

namespace PasswordGenerator
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(EditItemPage), typeof(EditItemPage));

            Routing.RegisterRoute(nameof(AddPasswordPage), typeof(AddPasswordPage));

        }

    }
}
