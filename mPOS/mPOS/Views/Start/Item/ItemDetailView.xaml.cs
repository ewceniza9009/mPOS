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
    public partial class ItemDetailView : TabbedPage
    {
        private ItemViewModel vm;
        public ItemDetailView(ItemViewModel vm)
        {
            InitializeComponent();

            BindingContext = this.vm = vm;

            foreach (var child in Children)
            {
                child.BindingContext = this.vm;
            }
        }
    }
}