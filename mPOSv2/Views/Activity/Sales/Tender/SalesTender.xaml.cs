using mPOSv2.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOSv2.Views.Activity.Sales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesTender : ContentPage
    {
        #region Initialize
        public SalesTender(SalesViewModel vm)
        {
            InitializeComponent();

            BindingContext = this.vm = vm;
        }
        #endregion

        #region Properties
        public readonly SalesViewModel vm;
        #endregion

        #region Events
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            vm.NewTender.TenderAmount = vm.NewTender.TrnCollectionLines.Sum(x => x.Amount);
            vm.NewTender.ChangeAmount = vm.NewTender.TrnCollectionLines.Sum(x => x.Amount) - vm.SelectedSale.TrnSalesLines.Sum(x => x.Amount);

            vm.RefreshTender();
        }

        private void CmdClose_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>  await Application.Current.MainPage.Navigation.PopAsync());
        }
        #endregion
    }
}