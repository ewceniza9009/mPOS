using System;
using mPOS.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void CmdLogin_Clicked(object sender, EventArgs e)
        {
            UserName.Text = "admin";
            Password.Text = "innosoft";

            var user = new POCO.MstUser
            {
                UserName = UserName.Text,
                Password = Password.Text
            };

            var canLogin = ApiRequest<POCO.MstUser, bool>.PostRead("MstUser/CanLogin", user);

            if (await canLogin)
            {
                await Navigation.PushAsync(new PosNavigation());
            }
            else
            {
                LoginText.Text = "Invalid username or password!";
            }
        }
    }
}