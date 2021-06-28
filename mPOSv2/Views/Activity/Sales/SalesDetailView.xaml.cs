using System;
using mPOSv2.Models.Page;
using mPOSv2.Services;
using mPOSv2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
using SelectionChangedEventArgs = Syncfusion.XForms.ComboBox.SelectionChangedEventArgs;

namespace mPOSv2.Views.Activity.Sales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesDetailView : ContentPage
    {
        #region Initialize

        public SalesDetailView(SalesViewModel vm)
        {
            var isNotTendered = vm.SelectedSale.IsNotTendered;

            InitializeComponent();

            BindingContext = this.vm = vm;

            CmdSearchBarcode.Clicked += CmdSearchBarcode_Clicked;

            vm.SelectedSale.IsNotTendered = isNotTendered;
        }

        #endregion

        //TODO : CachingStrategy="RecycleElement"

        #region Properties

        private readonly SalesViewModel vm;
        private ZXingScannerPage scanPage;

        #endregion

        #region Events

        private async void CmdSearchBarcode_Clicked(object sender, EventArgs e)
        {
            if (!SettingsRepository.GetSettings().ContinuesBarcode)
            {
                scanPage = new ZXingScannerPage();

                scanPage.OnScanResult += result =>
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
                scanPage = new ZXingScannerPage(new MobileBarcodeScanningOptions
                {
                    DelayBetweenContinuousScans = 1000
                });

                scanPage.OnScanResult += result =>
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

        private void UnitComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.ExecuteSelectCustomer(new object());
        }

        private void ButtonPagePrev_OnClicked(object sender, EventArgs e)
        {
            if (Pager.CurrentPage != 1) Pager.CurrentPage--;

            vm.ReloadSalesLines();
        }

        private void ButtonPageNext_OnClicked(object sender, EventArgs e)
        {
            var endPage = Pager.EndPage = vm.GetEndPage();

            if (Pager.CurrentPage != (int) endPage) Pager.CurrentPage++;

            vm.ReloadSalesLines();
        }
        #endregion
    }
}