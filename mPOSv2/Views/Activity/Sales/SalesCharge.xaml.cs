using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


using Rg.Plugins.Popup.Services;
using mPOSv2.ViewModels;

namespace mPOSv2.Views.Activity.Sales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesCharge
    {
        public readonly SalesViewModel vm;

        public SalesCharge(SalesViewModel vm)
        {
            InitializeComponent();

            BindingContext = this.vm = vm;
        }

        private void Term_OnSelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {

        }

        private void CmdOk_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void PopupPage_Disappearing(object sender, EventArgs e)
        {
            vm.IsSalesChargeModalShown = false;
        }
    }
}