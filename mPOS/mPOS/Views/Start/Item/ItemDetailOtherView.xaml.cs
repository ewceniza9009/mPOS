using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mPOS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOS.Views.Start
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailOtherView : ContentPage
    {
        #region properties
        public ItemViewModel vm; 
        #endregion

        #region ctor
        public ItemDetailOtherView()
        {
            InitializeComponent();
        } 
        #endregion

        #region Events
        private void ItemDetailOtherView_OnAppearing(object sender, EventArgs e)
        {
            vm = BindingContext as ItemViewModel;
        }

        private void MarkUpEntry_Unfocused(object sender, FocusEventArgs e)
        {
            CalculatePrice();
        }

        private void CostEntry_Unfocused(object sender, FocusEventArgs e)
        {
            CalculatePrice();
        }

        private void StockLevelQtyStepper_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            vm?.ExecuteRefreshSelectedItem(new object());
        }
        #endregion

        #region Methods
        private void CalculatePrice()
        {
            var cost = (vm.SelectedItem?.Cost ?? 0) == 0 ? 1 : vm.SelectedItem.Cost;
            var markUpAmount = cost * ((vm.SelectedItem?.MarkUp ?? 0) / 100);

            if (vm.SelectedItem != null)
            {
                vm.SelectedItem.Price = cost + markUpAmount;

                vm.ExecuteRefreshSelectedItem(new object());
            }
        }
        #endregion

    }
}