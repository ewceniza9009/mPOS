using System;
using System.Collections.Generic;
using mPOSv2.MenuItems;
using mPOSv2.Services;
using mPOSv2.Views.Activity.Sales;
using mPOSv2.Views.Report;
using mPOSv2.Views.Setup.Customer;
using mPOSv2.Views.Setup.Item;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace mPOSv2.Views.Start
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();

            MenuList = new List<MenuPageItem>
            {
                new MenuPageItem
                {
                    Title = "Sales",
                    Description = "Sales transaction per day",
                    Icon = "sales.png",
                    TargetType = typeof(SalesView)
                },
                new MenuPageItem
                {
                    Title = "Customer",
                    Description = "All active and non-active customers",
                    Icon = "customer.png",
                    TargetType = typeof(CustomerView)
                },
                new MenuPageItem
                {
                    Title = "Item",
                    Description = " All inventory and non-inventory items",
                    Icon = "product.png",
                    TargetType = typeof(ItemView)
                },
                new MenuPageItem
                {
                    Title = "Report",
                    Description = " All reports for POS",
                    Icon = "purchase_order.png",
                    TargetType = typeof(Reports)
                },
                new MenuPageItem
                {
                    Title = "Settings",
                    Description = "POS Settings",
                    Icon = "setting.png",
                    TargetType = typeof(Settings)
                },
                new MenuPageItem
                {
                    Title = "Logout",
                    Description = "Login to other accounts",
                    Icon = "logout.png",
                    TargetType = typeof(LogoutPage)
                }
            };

            MenuItems.ItemsSource = MenuList;
        }

        public List<MenuPageItem> MenuList { get; set; }

        private void StartPage_OnAppearing(object sender, EventArgs e)
        {
            WelcomeLabel.Text = $"WELCOME {SettingsRepository.GetUserFullName().ToUpper()}!";
        }

        private async void MenuItems_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (MenuPageItem) e.ItemData;
            var page = item.TargetType;
            var appMenu = new AppMenu();

            if (page != typeof(LogoutPage))
            {
                await Navigation.PushAsync(appMenu);

                appMenu.Detail = new NavigationPage((Page) Activator.CreateInstance(page));
                appMenu.IsPresented = false;
            }
            else
            {
                await Navigation.PushAsync(new Login());
            }
        }
    }
}