using System;
using System.Collections.Generic;
using System.Linq;
                                                                 using System.Text;
using System.Threading.Tasks;
using mPOS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ZXing.Net.Mobile.Forms;

namespace mPOS.Views.Start
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailGeneralView : ContentPage
    {
        public ItemViewModel vm;
        ZXingScannerPage scanPage;

        public ItemDetailGeneralView() : base()
        {
            InitializeComponent();

            CmdScanBarcode.Clicked += CmdScanBarcode_Clicked;
        }

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

        
    }
}