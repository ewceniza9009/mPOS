using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mPOS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOS.Views.Activities.Sales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesDetailView : ContentPage
    {
        private SalesViewModel vm;
        public SalesDetailView(SalesViewModel vm)
        {
            InitializeComponent();

            BindingContext = this.vm = vm;
        }
    }
}