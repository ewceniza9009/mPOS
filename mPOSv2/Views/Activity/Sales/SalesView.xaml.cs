using System;
using mPOSv2.Models.Page;
using mPOSv2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOSv2.Views.Activity.Sales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesView : ContentPage
    {
        #region Properties
        private SalesViewModel vm;
        #endregion

        #region Initialize
        public SalesView()
        {
            InitializeComponent();

            vm = new SalesViewModel();
            BindingContext = vm;
        }
        #endregion

        #region Events
        private void SalesView_OnAppearing(object sender, EventArgs e)
        {
            vm = new SalesViewModel();
            BindingContext = vm;

            vm.Load();

            Pager.CurrentPage = 1;
        }

        private void SearchSale_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchSale.Text)) vm.Load();
        }

        private void SearchSaleDate_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            vm.ExecuteSearch(new object());
        }
        #endregion
    }
}