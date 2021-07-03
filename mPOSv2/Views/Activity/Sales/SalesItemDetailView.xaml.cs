using System;
using System.Linq;
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
        private readonly SalesViewModel vm;
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
                taxAmount = Math.Round(
                    amount / (1 + vm.SelectedSaleLine.TaxRate / 100) * (vm.SelectedSaleLine.TaxRate / 100), 2);
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

        private void DiscountComboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var discount = vm.SelectedDiscount;

            if (discount.Discount == "Senior Citizen Discount")
            {
                var price = 0m;
                var discountAmount = 0m;

                vm.SelectedSaleLine.DiscountRate = discount.DiscountRate;

                if (vm.SelectedSaleLine.TaxRate > 0)
                {
                    price = Math.Round(vm.SelectedSaleLine.Price - Math.Round(vm.SelectedSaleLine.Price / (1 + vm.SelectedSaleLine.TaxRate / 100) * (vm.SelectedSaleLine.TaxRate / 100), 2), 2);
                }
                else 
                {
                    price = Math.Round(vm.SelectedSaleLine.Price, 2);
                }

                discountAmount = Math.Round(price * Math.Round(discount.DiscountRate / 100, 2), 2);
                vm.SelectedSaleLine.DiscountAmount = Math.Round(discountAmount, 2);
                vm.SelectedSaleLine.NetPrice = Math.Round(vm.SelectedSaleLine.Price, 2) - Math.Round(discountAmount, 2);
            }
            else 
            {
                vm.SelectedSaleLine.DiscountRate = Math.Round(discount.DiscountRate, 2);
                vm.SelectedSaleLine.DiscountAmount = Math.Round(vm.SelectedSaleLine.Price, 2) * Math.Round(discount.DiscountRate / 100, 2);
                vm.SelectedSaleLine.NetPrice = Math.Round(vm.SelectedSaleLine.Price, 2) - Math.Round(vm.SelectedSaleLine.DiscountAmount, 2);

                vm.SelectedSaleLine.Amount = vm.ComputeAmount();
                vm.SelectedSaleLine.TaxAmount = vm.ComputeVatAmount();

                if (discount.Discount == "Variable Discount")
                {
                    vm.CanEditDiscount = false;
                }
                else 
                {
                    vm.CanEditDiscount = true;
                }
            }

            vm.ExecuteRefreshSelectedSaleLine(new object());
        }

        private void DiscountAmount_Completed(object sender, EventArgs e)
        {
            if (vm.SelectedSaleLine.DiscountAmount <= vm.SelectedSaleLine.Price)
            {
                vm.SelectedSaleLine.DiscountRate = Math.Round((vm.SelectedSaleLine.DiscountAmount / vm.SelectedSaleLine.Price) * 100, 2);
                vm.SelectedSaleLine.NetPrice = Math.Round(vm.SelectedSaleLine.Price, 2) - Math.Round(vm.SelectedSaleLine.DiscountAmount, 2);

                vm.SelectedSaleLine.Amount = vm.ComputeAmount();
                vm.SelectedSaleLine.TaxAmount = vm.ComputeVatAmount();

                if (vm.SelectedSaleLine.NetPrice < vm.Items.ToList().SingleOrDefault(x => x.Id == vm.SelectedSaleLine.ItemId).Cost)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                        await Application.Current.MainPage.DisplayAlert(vm.Title, "Net price is now lesser than item cost.  Proceed anyway?.", "Yes", "No").ContinueWith(x =>
                        {
                            if (!x.Result)
                            {
                                vm.SelectedSaleLine.DiscountAmount = 0;
                                vm.SelectedSaleLine.DiscountRate = 0;

                                vm.SelectedSaleLine.NetPrice = Math.Round(vm.SelectedSaleLine.Price, 2) - Math.Round(vm.SelectedSaleLine.DiscountAmount, 2);

                                vm.SelectedSaleLine.Amount = vm.ComputeAmount();
                                vm.SelectedSaleLine.TaxAmount = vm.ComputeVatAmount();

                                vm.ExecuteRefreshSelectedSaleLine(new object());
                            }
                        },
                        TaskScheduler.FromCurrentSynchronizationContext())
                    );
                }
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.DisplayAlert(vm.Title, "Discount amount exceeds item price.", "Ok"));

                vm.SelectedSaleLine.DiscountAmount = 0;
                vm.SelectedSaleLine.DiscountRate = 0;

                vm.SelectedSaleLine.NetPrice = Math.Round(vm.SelectedSaleLine.Price, 2) - Math.Round(vm.SelectedSaleLine.DiscountAmount, 2);

                vm.ComputeAmount();
            }

            vm.ExecuteRefreshSelectedSaleLine(new object());
        }

        private void CmdOK_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync().ContinueWith(x =>
            {
                if (vm.ItemFrom == ItemFrom.Item) vm.SelectedSale.TrnSalesLines.Add(vm.SelectedSaleLine);

                vm.ReloadSalesLines();
            });
        }
    }
}