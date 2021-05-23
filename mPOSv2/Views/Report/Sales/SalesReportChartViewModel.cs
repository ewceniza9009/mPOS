using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace mPOSv2.Views.Report.Sales
{
    public class SalesReportChartViewModel
    {
        public ObservableCollection<SalesReport> Data { get; set; }

        public SalesReportChartViewModel()
        {
            Data = new ObservableCollection<SalesReport>()
            {
                new SalesReport("Jan", 50),
                new SalesReport("Feb", 70),
                new SalesReport("Mar", 65),
                new SalesReport("Apr", 57),
                new SalesReport("May", 48),
            };
        }
    }

    public class SalesReport
    {
        public string Month { get; set; }

        public double Target { get; set; }

        public SalesReport(string xValue, double yValue)
        {
            Month = xValue;
            Target = yValue;
        }
    }    
}
