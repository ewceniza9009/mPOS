using mPOSv2.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOSv2.Views.Activity.Sales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesTenderLine
    {
        #region Initialize
        public SalesTenderLine(SalesViewModel vm)
        {
            InitializeComponent();

            BindingContext = this.vm = vm;
        }
        #endregion

        #region Properties
        public readonly SalesViewModel vm;
        #endregion

        #region Events

        private void CmdOk_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void PopupPage_Disappearing(object sender, EventArgs e)
        {

        } 
        #endregion
    }
}