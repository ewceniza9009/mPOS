using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mPOS.Services;
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
            if (!SettingsRepository.GetSettings().ContinuesBarcode)
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
            else
            {
                scanPage = new ZXingScannerPage(new ZXing.Mobile.MobileBarcodeScanningOptions
                {
                    DelayBetweenContinuousScans = 1000
                });

                scanPage.OnScanResult += (result) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                        await DisplayAlert("Scanned Barcode", result.Text, "OK"));

                    vm.SearchBarcode = result.Text;
                    vm.ExecuteSelectItemByContinuesBarCode();
                    vm.ExecuteRefreshSelectedSale(new object());
                };

                await Navigation.PushModalAsync(scanPage);
            }
        }

        private void CmdSearchItem_OnClicked(object sender, EventArgs e)
        {
            vm.ExecuteShowItems();
        }

        private void SalesLineListView_OnBindingContextChanged(object sender, EventArgs e)
        {
            //TODO: Trap Caching Strategy Error
        }
    }
}