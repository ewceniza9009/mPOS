using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mPOS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOS.Views.Activities
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesView : ContentPage
    {
        private SalesViewModel vm;
        public SalesView()
        {
            InitializeComponent();

            vm = new SalesViewModel();
            BindingContext = vm;
        }

        private void SalesView_OnAppearing(object sender, EventArgs e)
        {
            vm.Load();
        }
    }
}