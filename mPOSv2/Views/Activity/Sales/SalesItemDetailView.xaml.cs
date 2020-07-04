using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mPOSv2.Enums;
using mPOSv2.ViewModels;
using Syncfusion.SfNumericUpDown.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOSv2.Views.Activity.Sales
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

        private void QuantityStepper_OnValueChanged(object sender, ValueEventArgs e)
        {
            var taxAmount = 0m;
            var amount = Math.Round(vm.SelectedSaleLine.NetPrice * vm.SelectedSaleLine.Quantity, 2);

            if (vm.SelectedTax.Code == "INCLUSIVE")
            {
                taxAmount = Math.Round((amount/ (1 + vm.SelectedSaleLine.TaxRate / 100)) * (vm.SelectedSaleLine.TaxRate / 100), 2);
            }
            else
            {
                taxAmount = Math.Round(amount * (vm.SelectedSaleLine.TaxRate / 100), 2);
                amount = Math.Round(amount + taxAmount, 2);
            }

            vm.SelectedSaleLine.Amount = amount;
            vm.SelectedSaleLine.TaxAmount = taxAmount;
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