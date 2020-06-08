using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PosNavigation : MasterDetailPage
    {
        public List<MenuItems.MenuPageItem> MenuList { get; set; }

        public PosNavigation()
        {
            InitializeComponent();


            MenuList = new List<MenuItems.MenuPageItem>
            {
                new MenuItems.MenuPageItem { Title = "Home", Icon = "home.png", TargetType = typeof(Start.CustomerView) },
                new MenuItems.MenuPageItem { Title = "Setting", Icon = "setting.png", TargetType = typeof(Start.Setting) },
                new MenuItems.MenuPageItem { Title = "Help", Icon = "help.png", TargetType = typeof(Start.HelpPage) },
                new MenuItems.MenuPageItem { Title = "LogOut", Icon = "logout.png", TargetType = typeof(Start.LogoutPage) }
            };

            NavigationDrawerList.ItemsSource = MenuList;

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Start.CustomerView)));
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MenuItems.MenuPageItem)e.SelectedItem;
            var page = item.TargetType;

            if (page != typeof(Start.LogoutPage))
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(page));
                IsPresented = false;
            }
            else
            {
                await Navigation.PushAsync(new Login());
            }
        }

        private void CmdHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PosNavigation());
        }
    }
}