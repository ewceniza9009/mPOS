using System;
using System.Collections.Generic;
using mPOSv2.MenuItems;
using mPOSv2.Services;
using mPOSv2.Views.Activity.Sales;
using mPOSv2.Views.Report;
using mPOSv2.Views.Setup.Customer;
using mPOSv2.Views.Setup.Item;
using mPOSv2.Views.Start;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace mPOSv2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppMenu
    {
        public AppMenu()
        {
            InitializeComponent();

            MenuList = new List<MenuPageItem>
            {
                new MenuPageItem {Title = "Sales", Icon = "sales.png", TargetType = typeof(SalesView)},
                new MenuPageItem {Title = "Customer", Icon = "customer.png", TargetType = typeof(CustomerView)},
                new MenuPageItem {Title = "Item", Icon = "product.png", TargetType = typeof(ItemView)},
                new MenuPageItem {Title = "Reports", Icon = "purchase_order.png", TargetType = typeof(Reports)},
                new MenuPageItem {Title = "Settings", Icon = "setting.png", TargetType = typeof(Settings)},
                new MenuPageItem {Title = "Logout", Icon = "logout.png", TargetType = typeof(LogoutPage)}
            };

            NavigationDrawerList.ItemsSource = MenuList;

            Detail = new NavigationPage((Page) Activator.CreateInstance(typeof(StartPage)));
        }

        public List<MenuPageItem> MenuList { get; set; }

        private async void NavigationDrawerList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (MenuPageItem) e.ItemData;
            var page = item.TargetType;

            if (page != typeof(LogoutPage))
            {
                Detail = new NavigationPage((Page) Activator.CreateInstance(page));
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