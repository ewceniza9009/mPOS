using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOSv2.Views.Report.Sales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesReportByCustomerInAMonth : ContentPage
    {
        private SalesReportByCustomerInAMonthViewModel vm;
        public SalesReportByCustomerInAMonth()
        {
            InitializeComponent();

            vm = new SalesReportByCustomerInAMonthViewModel();

            BindingContext = vm;
        }
    }
}