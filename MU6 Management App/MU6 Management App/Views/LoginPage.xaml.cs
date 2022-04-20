using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MU6_Management_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnSignup_Clicked(object sender, EventArgs e)
        {
            GoToNextPage(new SignupPage());
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            if (await Database.Login.Validate(txtUsername.Text, txtPassword.Text))
            {
                GoToNextPage(new UserPage());
            }
        }

        private async void GoToNextPage(Page page)
        {
            txtUsername.Text = txtPassword.Text = "";
            await Navigation.PushAsync(page);
        }
    }
}