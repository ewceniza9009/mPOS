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

            vm = new CustomerViewModel();
            BindingContext = vm;
        }
        #endregion

        #region Events
        private void HomePage_OnAppearing(object sender, EventArgs e)
        {
            vm.Load();
        }
        #endregion
    }
}