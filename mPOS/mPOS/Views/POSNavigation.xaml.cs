﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using mPOS.Services;
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
                new MenuItems.MenuPageItem { Title = "Sales", Icon = "sales.png", TargetType = typeof(Activities.SalesView) },
                new MenuItems.MenuPageItem { Title = "Customer", Icon = "customer.png", TargetType = typeof(Start.CustomerView) },
                new MenuItems.MenuPageItem { Title = "Item", Icon = "product.png", TargetType = typeof(Start.ItemView) },
                new MenuItems.MenuPageItem { Title = "Settings", Icon = "setting.png", TargetType = typeof(Start.Setting) },
                new MenuItems.MenuPageItem { Title = "Logout", Icon = "logout.png", TargetType = typeof(Start.LogoutPage) }
            };

            NavigationDrawerList.ItemsSource = MenuList;

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Activities.SalesView)));
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

        private void PosNavigation_OnDisappearing(object sender, EventArgs e)
        {
            Disappearing -= PosNavigation_OnDisappearing;
        }

        private void PosNavigation_OnAppearing(object sender, EventArgs e)
        {
            UserFullName.Text = SettingsRepository.GetUserFullName();
        }
    }
}