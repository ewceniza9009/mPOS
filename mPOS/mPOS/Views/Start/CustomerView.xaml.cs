using mPOS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mPOS.POCO;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace mPOS.Views.Start
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerView : ContentPage
    {
        #region Properties
        private CustomerViewModel vm;
        #endregion

        #region Initialize
        public CustomerView()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void HomePage_OnAppearing(object sender, EventArgs e)
        {
            vm = new CustomerViewModel();
            BindingContext = vm;

            vm.Load();
        }

        private async void CustomerListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if ((sender as ListView)?.SelectedItem is MstCustomer selectedCustomer)
            {
                vm.IsChanged = false;
                vm.SelectedCustomer = selectedCustomer;

                await Navigation.PushAsync(new CustomerDetailView(vm));
            }
        }

        private async void CmdAdd_OnClicked(object sender, EventArgs e)
        {
            var newCustomer = new MstCustomer();

            vm.Customers.Add(newCustomer);
            vm.SelectedCustomer = newCustomer;

            await Navigation.PushAsync(new CustomerDetailView(vm));
        }

        private void CmdSearch_OnClicked(object sender, EventArgs e)
        {
            vm.Load(SearchCustomer.Text);
        }
        #endregion

    }
}