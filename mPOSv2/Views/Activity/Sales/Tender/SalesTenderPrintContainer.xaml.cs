using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOSv2.Views.Activity.Sales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesTenderPrintContainer 
    {
        public int SaleId { get; set; }
        public string CallerName { get; set; }
        public SalesTenderPrintContainer(int salesId)
        {
            InitializeComponent();

            SaleId = salesId;

            var stackTrace = new StackTrace();
            var caller = stackTrace.GetFrame(1).GetMethod();

            CallerName = caller.Name;

            this.Appearing += SalesTenderPrintContainer_Appearing;
        }

        private void SalesTenderPrintContainer_Appearing(object sender, EventArgs e)
        {
            MsgPrint.OnLoadAction(sender);
        }
    }
}