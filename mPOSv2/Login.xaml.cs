using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mPOS.POCO;
using mPOSv2.Services;
using mPOSv2.Models;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOSv2
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

            var user = new MstUser
            {
                UserName = UserName.Text,
                Password = Password.Text
            };

            var login = await ApiRequest<MstUser, MstUser>.PostRead("MstUser/CanLogin", user);

            if (login.Id != 0)
            {
                Settings settings;

                using (var conn = new SQLiteConnection(App.FilePath))
                {
                    settings = new Models.Settings()
                    {
                        UserId = 1,
                        UserFullName = login.FullName,
                        ServerName = "192.168.1.8",
                        LoginDate = DateTime.Now,
                        ContinuesBarcode = false
                    };

                    var tableExistsQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Settings';";
                    var result = conn.ExecuteScalar<string>(tableExistsQuery);

                    if (string.IsNullOrEmpty(result))
                    {
                        conn.CreateTable<Models.Settings>();
                        conn.Insert(settings);
                    }
                    else
                    {
                        settings = conn.Query<Models.Settings>("SELECT * FROM Settings WHERE Id = ?", 1).FirstOrDefault();

                        if (settings != null)
                        {
                            settings.UserId = login.Id;
                            settings.UserFullName = login.FullName;
                            settings.ServerName = "192.168.1.8";
                            settings.LoginDate = DateTime.Now;

                            conn.RunInTransaction(() =>
                            {
                                if (conn != null) conn.Update(settings);
                            });
                        }
                    }
                }

                await Navigation.PopAsync();
                await Navigation.PushAsync(new AppMenu());
            }
            else
            {
                LoginText.Text = "Invalid username or password!";
            }
        }
    }
}