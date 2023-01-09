using System;
using System.Threading.Tasks;
using mPOSv2.Models.Page;
using mPOSv2.Services;
using mPOSv2.ViewModels;
using Rg.Plugins.Popup.Services;
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
            InitializeBackButtonAction();

            BindingContext = this.vm = vm;

            CmdSearchBarcode.Clicked += CmdSearchBarcode_Clicked;

            vm.SelectedSale.IsNotTendered = isNotTendered;
            vm.SelectedSaleTracker.ChangedProperties.Clear();
        }
        #endregion

        #region Properties
        public readonly SalesViewModel vm;
        private ZXingScannerPage scanPage;
        public Action BackButtonAction { get; set; }
        public static readonly BindableProperty EnableBackButtonOverrideProperty = BindableProperty.Create(nameof(EnableBackButtonOverride), typeof(bool), typeof(SalesDetailView), false);

        public bool EnableBackButtonOverride
        {
            get
            {
                return (bool)GetValue(EnableBackButtonOverrideProperty);
            }
            set
            {
                SetValue(EnableBackButtonOverrideProperty, value);
            }
        }
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
                vm.IsBarcodeModalShown = true;
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
                        await Application.Current.MainPage.DisplayAlert("Scanned Barcode", result.Text, "OK"));

                    System.Threading.Thread.Sleep(1000);

                    vm.SearchBarcode = result.Text;
                    vm.ExecuteSelectItemByContinuesBarCode();
                    vm.ExecuteRefreshSelectedSale(new object());
                };

                await Navigation.PushModalAsync(scanPage);
                vm.IsBarcodeModalShown = true;
            }
        }

        private void CmdSearchItem_OnClicked(object sender, EventArgs e)
        {
            vm.ExecuteShowItems();
        }

        private void CustomerComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.ExecuteSelectCustomer(new object());
        }

        private void CmdMore_Clicked(object sender, EventArgs e)
        {
            vm.IsSalesChargeModalShown = true;
            PopupNavigation.Instance.PushAsync(new SalesCharge(vm));
        }

        private void ButtonPagePrev_OnClicked(object sender, EventArgs e)
        {
            var hasChanges = false;

            if (Pager.CurrentPage != 1) Pager.CurrentPage--;

            if (vm.IsCollectionChanged || (vm.SelectedSaleTracker?.ChangedProperties != null && vm.SelectedSaleTracker.ChangedProperties.Count > 0))
            {
                hasChanges = true;
            }

            vm.ReloadSalesLines();

            if (!vm.SelectedSale.IsNotTendered)
            {
                ClearChangeTracker();
            }
            else
            {
                if (!hasChanges)
                {
                    ClearChangeTracker();
                }
            }
            
        }

        private void ButtonPageNext_OnClicked(object sender, EventArgs e)
        {
            var endPage = Pager.EndPage = vm.GetEndPage();
            var hasChanges = false;

            if (Pager.CurrentPage != (int) endPage) Pager.CurrentPage++;

            if (vm.IsCollectionChanged || (vm.SelectedSaleTracker?.ChangedProperties != null && vm.SelectedSaleTracker.ChangedProperties.Count > 0))
            {
                hasChanges = true;
            }

            vm.ReloadSalesLines();

            if (!vm.SelectedSale.IsNotTendered)
            {
                ClearChangeTracker();
            }
            else
            {
                if (!hasChanges)
                {
                    ClearChangeTracker();
                }
            }
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            CmdSave.IsEnabled = vm.SelectedSale?.IsNotTendered ?? true;
            CmdTender.IsEnabled = vm.SelectedSale?.IsNotTendered ?? true;
            CmdDelete.IsEnabled = vm.SelectedSale?.IsNotTendered ?? true;
        }
        #endregion

        #region Methods
        private void InitializeBackButtonAction()
        {
            if (EnableBackButtonOverride)
            {
                BackButtonAction = OnBackButtonAction;
            }
        }

        private void OnBackButtonAction() 
        {
            //Please check error on debug, stepper cannot proceed in this area
            if (vm.IsBarcodeModalShown)
            {
                vm.IsBarcodeModalShown = false;
                Navigation.PopModalAsync();
            }
            else if (vm.IsSalesChargeModalShown)
            {
                vm.IsSalesChargeModalShown = false;
                if(PopupNavigation.Instance.PopupStack.Count > 0) PopupNavigation.Instance.PopAsync();
            }
            else
            {
                var isDirty = vm.IsCollectionChanged || (vm.SelectedSaleTracker?.ChangedProperties == null ? false : vm.SelectedSaleTracker.ChangedProperties.Count > 0);

                if (isDirty)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                        await Application.Current.MainPage.DisplayAlert(vm.Title, "Record has been changed.  Proceed anyway?.", "Yes", "No").ContinueWith(x =>
                        {
                            if (x.Result)
                            {
                                Navigation.PopAsync(true);
                            }
                        },
                        TaskScheduler.FromCurrentSynchronizationContext())
                    );
                }
                else
                {
                    Navigation.PopAsync(true);
                }
            }
        }

        public void ClearChangeTracker() 
        {
            vm.IsCollectionChanged = false;
            if (vm.SelectedSaleTracker?.ChangedProperties != null) vm.SelectedSaleTracker.ChangedProperties.Clear();
        }

        #endregion
    }
}