using mPOSv2.ViewModels;
using mPOSv2.Views.Report.Sales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace mPOSv2.Views.Report
{
    public class SalesReportChartViewModel : ViewModelBase
    {
        public ObservableCollection<ReportItem> ReportList
        {
            get => _ReportList; 
            set => _ReportList = value; 
        }
        private ObservableCollection<ReportItem> _ReportList;

        public SalesReportChartViewModel()
        {
            // Initialize with default values
            ReportList = new ObservableCollection<ReportItem>
            {
                new ReportItem { SortOrder = 1,  Text = "Sales report by customer in a month"},
                new ReportItem { SortOrder = 2,  Text = "Sales chart"}
            };
        }


        public Command CmdOpenSalesReport
        {
            get => _CmdOpenSalesReport ?? (_CmdOpenSalesReport = new Command(ExecutOpeneSalesReport, x => true));
            set => SetProperty(ref _CmdOpenSalesReport, value);
        }

        private Command _CmdOpenSalesReport;

        private void ExecutOpeneSalesReport(object sender)
        {
            var selected = sender as ReportItem;

            switch (selected.SortOrder) 
            {
                case 1:
                    Device.BeginInvokeOnMainThread(async () =>
                    await Application.Current.MainPage.Navigation.PushAsync(new SalesReportByCustomerInAMonth()));
                    break;
                case 2:
                    Device.BeginInvokeOnMainThread(async () =>
                    await Application.Current.MainPage.Navigation.PushAsync(new SalesReportChart()));
                    break;
            }
        }
    }

    public class ReportItem : ViewModelBase
    {
        public int SortOrder
        {
            get => _ID;
            set => SetProperty(ref _ID, value);
        }
        private int _ID;

        public string Text
        {
            get => _Text;
            set => SetProperty(ref _Text, value);
        }
        private string _Text;
    }
}
