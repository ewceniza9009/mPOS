using mPOSv2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOSv2.Views.Activity.Sales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesItemView : ContentPage
    {
        private readonly SalesViewModel vm;

        public SalesItemView(SalesViewModel vm)
        {
            InitializeComponent();

            BindingContext = this.vm = vm;
        }

        private void SearchItem_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchItem.Text)) vm.ExecuteSearchItem(new object());
        }
    }
}