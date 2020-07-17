using System;
using mPOSv2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOSv2.Views.Setup.Item
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemView : ContentPage
    {
        private readonly ItemViewModel vm;

        public ItemView()
        {
            InitializeComponent();

            vm = new ItemViewModel();
            BindingContext = vm;
        }

        private void ItemView_OnAppearing(object sender, EventArgs e)
        {
            vm.Load();
        }

        private void SearchItem_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchItem.Text)) vm.Load();
        }
    }
}