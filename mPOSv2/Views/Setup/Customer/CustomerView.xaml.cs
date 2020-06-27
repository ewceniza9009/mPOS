using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mPOSv2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOSv2.Views.Setup.Customer
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

            vm = new CustomerViewModel();
            BindingContext = vm;
        }
        #endregion

        #region Events
        private void HomePage_OnAppearing(object sender, EventArgs e)
        {
            vm.Load();
        }

        private void SearchItem_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchItem.Text))
            {
                vm.Load();
            }
        }
        #endregion
    }
}