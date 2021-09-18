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
    public partial class SalesTenderPrintContainer 
    {
        public int SaleId { get; set; }
        public SalesTenderPrintContainer(int salesId)
        {
            InitializeComponent();

            SaleId = salesId;
        }
    }
}