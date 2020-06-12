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
    public partial class ItemView : ContentPage
    {
        private ItemViewModel vm;

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
    }
}