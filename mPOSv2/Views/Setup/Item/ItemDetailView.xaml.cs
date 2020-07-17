using mPOSv2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace mPOSv2.Views.Setup.Item
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailView : TabbedPage
    {
        private readonly ItemViewModel vm;

        public ItemDetailView(ItemViewModel vm)
        {
            InitializeComponent();

            BindingContext = this.vm = vm;

            Children.ForEach(child => { child.BindingContext = this.vm; });
        }
    }
}