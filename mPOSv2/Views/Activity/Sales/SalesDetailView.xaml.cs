using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mPOSv2.Services;
using mPOSv2.ViewModels;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace mPOSv2.Views.Activity.Sales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesDetailView : ContentPage
    {
        //TODO : CachingStrategy="RecycleElement"

        #region Properties
        private SalesViewModel vm;
        ZXingScannerPage scanPage;
        #endregion
        
        #region Initialize
        public SalesDetailView(SalesViewModel vm)
        {
            InitializeComponent();

            BindingContext = this.vm = vm;

            CmdSearchBarcode.Clicked += CmdSearchBarcode_Clicked;
        } 
        #endregion

        #region Events
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

        private void UnitComboBox_OnSelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            vm.ExecuteSelectCustomer(new object());
        } 
        #endregion
    }
}