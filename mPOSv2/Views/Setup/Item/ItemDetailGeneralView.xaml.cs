using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mPOSv2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using SelectionChangedEventArgs = Syncfusion.XForms.ComboBox.SelectionChangedEventArgs;
using ValueChangedEventArgs = Syncfusion.SfAutoComplete.XForms.ValueChangedEventArgs;

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

            CategoryComboBox.IsVisible = true;
            CategoryEntry.IsVisible = false;
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

        private void CategoryAutoComplete_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            vm.ExecuteRefreshSelectedItem(new object());
        }

        private void CategoryComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.ExecuteRefreshSelectedItem(new object());
        }

        private void CmdNewCategory_OnClicked(object sender, EventArgs e)
        {
            if (!vm.IsHideCategory)
            {
                vm.IsHideCategory = true;
                CategoryComboBox.IsVisible = !vm.IsHideCategory;
                CategoryEntry.IsVisible = vm.IsHideCategory;
                
            }
            else
            {
                vm.IsHideCategory = false;
                CategoryComboBox.IsVisible = !vm.IsHideCategory;
                CategoryEntry.IsVisible = vm.IsHideCategory;
            }
        }

        private void OutputTaxComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.SelectedItem.OutTaxId = vm.SelectedTax.Id;
            vm.ExecuteRefreshSelectedItem(new object());
        }
        #endregion

    }
}