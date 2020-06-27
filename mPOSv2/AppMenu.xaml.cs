using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mPOSv2.Services;
using mPOSv2.Views.Start;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace mPOSv2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppMenu
    {
        public List<MenuItems.MenuPageItem> MenuList { get; set; }
        public AppMenu()
        {
            InitializeComponent();

            MenuList = new List<MenuItems.MenuPageItem>
            {
                new MenuItems.MenuPageItem { Title = "Sales", Icon = "sales.png", TargetType = typeof(Views.Activity.Sales.SalesView)},
                new MenuItems.MenuPageItem { Title = "Customer", Icon = "customer.png", TargetType = typeof(Views.Setup.Customer.CustomerView) },
                new MenuItems.MenuPageItem { Title = "Item", Icon = "product.png", TargetType = typeof(Views.Setup.Item.ItemView) },
                new MenuItems.MenuPageItem { Title = "Settings", Icon = "setting.png", TargetType = typeof(Views.Start.Settings) },
                new MenuItems.MenuPageItem { Title = "Logout", Icon = "logout.png", TargetType = typeof(Views.Start.LogoutPage) }
            };

            NavigationDrawerList.ItemsSource = MenuList;

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Views.Start.StartPage)));
        }

        private async void NavigationDrawerList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (MenuItems.MenuPageItem)e.ItemData;
            var page = item.TargetType;

            if (page != typeof(Views.Start.LogoutPage))
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(page));
                IsPresented = false;
            }
            else
            {
                await Navigation.PushAsync(new Login());
            }
        }

        private void AppMenu_OnAppearing(object sender, EventArgs e)
        {
            UserFullName.Text = SettingsRepository.GetUserFullName();
        }
    }
}