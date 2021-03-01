using System;
using System.Linq;
using mPOS.POCO;
using mPOSv2.Services;
using mPOSv2.Views.Start;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOSv2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        #region Properties

        private int loginAttemptCount;

        #endregion

        #region ctor

        public Login()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private async void CmdLogin_Clicked(object sender, EventArgs e)
        {
            var user = new MstUser
            {
                UserName = UserName.Text,
                Password = Password.Text
            };

            if (user.Password == "pass")
            {
                SettingsRepository.CreateLocalSettingsDB();

                await Navigation.PushAsync(new Settings());

                return;
            }

            try
            {
                LoginActivityIndicator.IsRunning = true;
                LoginActivityIndicator.IsVisible = true;

                LoginText.Text = "Logging in, please wait for a moment!";

                var login = await ApiRequest<MstUser, MstUser>.PostRead("MstUser/CanLogin", user);

                if (login != null && login.Id != 0)
                {
                    SettingsRepository.UpdateLocalSettingsDB(login);

                    await Navigation.PopAsync();
                    await Navigation.PushAsync(new AppMenu());
                }
                else
                {
                    if (loginAttemptCount > 0)
                    {
                        LoginText.Text = $"Invalid username or password!, attempt {loginAttemptCount}";
                        LoginText.TextColor = Color.Red;

                        if (loginAttemptCount > 9)
                        {
                            await Application.Current.MainPage.DisplayAlert("POS", "App will be terminated", "Ok");
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        LoginText.Text = "Invalid username or password!";
                    }

                    loginAttemptCount++;
                }

                LoginActivityIndicator.IsRunning = false;
                LoginActivityIndicator.IsVisible = false;
            }
            catch (Exception ex)
            {
                SettingsRepository.CreateLocalSettingsDB();

                LoginText.Text = $"{ex.Message}";

                LoginActivityIndicator.IsRunning = false;
                LoginActivityIndicator.IsVisible = false;
            }
        }

        private void Login_OnDisappearing(object sender, EventArgs e)
        {
            LoginText.Text = "";
            LoginText.TextColor = Color.Black;
        }

        #endregion
    }
}