using System;
using mPOSv2.ViewModels;
using Syncfusion.SfNumericTextBox.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SelectionChangedEventArgs = Syncfusion.XForms.ComboBox.SelectionChangedEventArgs;
using TextChangedEventArgs = Syncfusion.XForms.RichTextEditor.TextChangedEventArgs;

namespace mPOSv2.Views.Setup.Item
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailOtherView : ContentPage
    {
        #region Properties
        public ItemViewModel vm;
        #endregion

        #region ctor
        public ItemDetailOtherView()
        {
            InitializeComponent();
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

        #region Events
        private void ItemDetailOtherView_OnAppearing(object sender, EventArgs e)
        {
            vm = BindingContext as ItemViewModel;
        }

        private void MarkUpEntry_OnValueChanged(object sender, ValueEventArgs e)
        {
            CalculatePrice();
        }

        private void CostEntry_OnValueChanged(object sender, ValueEventArgs e)
        {
            CalculatePrice();
        }

        private void StockLevelQtyStepper_OnValueChanged(object sender,
            Syncfusion.SfNumericUpDown.XForms.ValueEventArgs e)
        {
            vm?.ExecuteRefreshSelectedItem(new object());
        }

        private void UnitComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.ExecuteSelectUnit(new object());
            vm.ExecuteRefreshSelectedItem(new object());
        }

        private void RemarksRichText_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            vm?.ExecuteRefreshSelectedItem(new object());
        }
        #endregion
    }
}