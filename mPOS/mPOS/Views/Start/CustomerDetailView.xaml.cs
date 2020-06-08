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
    public partial class CustomerDetailView : ContentPage
    {
        private CustomerViewModel vm;
        public CustomerDetailView(CustomerViewModel vm)
        {
            InitializeComponent();

            BindingContext = this.vm = vm;

            this.PropertyChanged += Vm_PropertyChanged;
        }

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (vm != null && e.PropertyName == "SelectedCustomer" && vm.SelectedCustomer != null)
            {
                vm.IsChanged = true;
            }
        }

        private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            vm.RefreshSelectedCustomer();
        }
    }
}