using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mPOS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace mPOS.Views.Activities.Sales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesDetailView : ContentPage
    {
        private SalesViewModel vm;
        ZXingScannerPage scanPage;

        public SalesDetailView(SalesViewModel vm)
        {
            InitializeComponent();

            BindingContext = this.vm = vm;

            CmdSearchBarcode.Clicked += CmdSearchBarcode_Clicked;
        }

        private async void CmdSearchBarcode_Clicked(object sender, EventArgs e)
        {
            scanPage = new ZXingScannerPage();

            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;

                Navigation.PopModalAsync();

                vm.SearchBarcode = result.Text;
                vm.ExecuteSelectItemByBarCode();
                vm.ExecuteRefreshSelectedSale(new object());
            };

            await Navigation.PushModalAsync(scanPage);
        }

        private void CmdSearchItem_OnClicked(object sender, EventArgs e)
        {
            vm.ExecuteShowItems();
        }

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            vm.SelectedSale.TrnSalesLines.Remove(vm.SelectedSaleLine);
            vm.ReloadSalesLines();
        }
    }
}