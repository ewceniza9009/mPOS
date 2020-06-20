using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mPOS.Enums;
using mPOS.POCO;
using mPOS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOS.Views.Activities.Sales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesItemDetailView : ContentPage
    {
        private SalesViewModel vm;
        public SalesItemDetailView(SalesViewModel vm)
        {
            InitializeComponent();

            BindingContext = this.vm = vm;
        }

        private void QuantityStepper_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            vm.SelectedSaleLine.Amount = vm.SelectedSaleLine.NetPrice * vm.SelectedSaleLine.Quantity;
            vm.ExecuteRefreshSelectedSaleLine(new object());
        }

        private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            vm.SelectedSaleLine.Amount = vm.SelectedSaleLine.NetPrice * vm.SelectedSaleLine.Quantity;
            vm.ExecuteRefreshSelectedSaleLine(new object());
        }

        private void CmdOK_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync().ContinueWith(x =>
            {
                if (vm.ItemFrom == ItemFrom.Item)
                {
                    vm.SelectedSale.TrnSalesLines.Add(vm.SelectedSaleLine);
                }

                vm.ReloadSalesLines();
            });
        }
    }
}