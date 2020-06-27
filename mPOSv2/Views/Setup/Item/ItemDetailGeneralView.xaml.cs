using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mPOSv2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace mPOSv2.Views.Setup.Item
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailGeneralView : ContentPage
    {
        #region Properties
        public ItemViewModel vm;
        ZXingScannerPage scanPage;
        #endregion

        #region ctor
        public ItemDetailGeneralView()
        {
            InitializeComponent();

            CmdScanBarcode.Clicked += CmdScanBarcode_Clicked;
        }
        #endregion

        #region Events
        private void ItemDetailGeneralView_OnAppearing(object sender, EventArgs e)
        {
            vm = BindingContext as ItemViewModel;
        }

        private async void CmdScanBarcode_Clicked(object sender, EventArgs e)
        {
            scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;

                Navigation.PopModalAsync();

                vm.SelectedItem.BarCode = result.Text;
                vm.ExecuteRefreshSelectedItem(new object());
            };

            await Navigation.PushModalAsync(scanPage);
        }
        #endregion
    }
}