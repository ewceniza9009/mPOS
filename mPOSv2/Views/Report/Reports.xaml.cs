using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOSv2.Views.Report
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reports : ContentPage
    {
        private SalesReportChartViewModel vm;
        public Reports()
        {
            InitializeComponent();

            vm = new SalesReportChartViewModel();

            BindingContext = vm;
        }
    }
}