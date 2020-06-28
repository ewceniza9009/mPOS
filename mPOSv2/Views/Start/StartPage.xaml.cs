using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mPOSv2.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace mPOSv2.Views.Start
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public List<MenuItems.MenuPageItem> MenuList { get; set; }
        public StartPage()
        {
            InitializeComponent();

            MenuList = new List<MenuItems.MenuPageItem>
            {
                new MenuItems.MenuPageItem
                {
                    Title = "Sales", 
                    Description = "Sales transaction per day",
                    Icon = "sales.png", 
                    TargetType = typeof(Views.Activity.Sales.SalesView)
                },
                new MenuItems.MenuPageItem
                {
                    Title = "Customer",
                    Description = "All active and non-active customers",
                    Icon = "customer.png", 
                    TargetType = typeof(Views.Setup.Customer.CustomerView)
                },
                new MenuItems.MenuPageItem 
                { 
                    Title = "Item", 
                    Description = " All inventory and non-inventory items",
                    Icon = "product.png", 
                    TargetType = typeof(Views.Setup.Item.ItemView)
                },
                new MenuItems.MenuPageItem
                {
                    Title = "Settings", 
                    Description = "POS Settings",
                    Icon = "setting.png", 
                    TargetType = typeof(Views.Start.Settings)
                },
                new MenuItems.MenuPageItem
                {
                    Title = "Logout", 
                    Description = "Login to other accounts",
                    Icon = "logout.png", 
                    TargetType = typeof(Views.Start.LogoutPage)
                }
            };

            MenuItems.ItemsSource = MenuList;

        }

        private void StartPage_OnAppearing(object sender, EventArgs e)
        {
            WelcomeLabel.Text = $"WELCOME {SettingsRepository.GetUserFullName().ToUpper()}!";
        }

        private async void MenuItems_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (MenuItems.MenuPageItem)e.ItemData;
            var page = item.TargetType;
            var appMenu = new AppMenu();

            if (page != typeof(Views.Start.LogoutPage))
            {
                await Navigation.PushAsync(appMenu);

                appMenu.Detail = new NavigationPage((Page)Activator.CreateInstance(page));
                appMenu.IsPresented = false;
            }
            else
            {
                await Navigation.PushAsync(new Login());
            }
        }
    }
}