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

            Models.Page.Pager.CurrentPage = 1;
        }

        private void SearchSale_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchSale.Text))
            {
                vm.Load();
            }
        }

        private void SearchSaleDate_OnDateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            vm.ExecuteSearch(new object());
        } 
        #endregion
    }
}