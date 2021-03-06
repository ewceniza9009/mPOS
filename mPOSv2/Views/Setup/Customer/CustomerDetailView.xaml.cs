﻿using mPOSv2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOSv2.Views.Setup.Customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerDetailView : ContentPage
    {
        private CustomerViewModel vm;

        public CustomerDetailView(CustomerViewModel vm)
        {
            InitializeComponent();

            BindingContext = this.vm = vm;
        }
    }
}