using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mPOSv2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DateChangedEventArgs = Syncfusion.XForms.Pickers.DateChangedEventArgs;

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
            vm.Load();
            //ShowListMessage();
        }

        private void SearchSale_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            vm.ExecuteSearch(new object());
            //ShowListMessage();
        }

        private void SearchSaleDate_OnDateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            vm.ExecuteSearch(new object());
            //ShowListMessage();
        } 
        #endregion

        #region Methods
        //public void ShowListMessage()
        //{
        //    if (vm.IsListEmpty)
        //    {
        //        SalesListMessage.IsVisible = true;
        //        SaleListStackLayout.IsVisible = false;
        //    }
        //    else
        //    {
        //        SalesListMessage.IsVisible = false;
        //        SaleListStackLayout.IsVisible = true;
        //    }
        //}
        #endregion
    }
}