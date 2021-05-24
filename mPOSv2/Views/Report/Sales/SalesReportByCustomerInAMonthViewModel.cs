using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mPOSv2.Views.Report.Sales
{
    public class SalesReportByCustomerInAMonthViewModel : ViewModels.ViewModelBase
    {
        public List<Models.Wrappers.Report.SalesReportByCustomerInAMonthWrapper> CustomerSales
        {
            get => _CustomerSales;
            set => SetProperty(ref _CustomerSales, value);
        }
        private List<Models.Wrappers.Report.SalesReportByCustomerInAMonthWrapper> _CustomerSales;

        public List<MonthWrapper> Months 
        {
            get => _Months;
            private set => SetProperty(ref _Months, value);
        }
        private List<MonthWrapper> _Months;

        public string CurrentMonth 
        {
            get => _CurrentMonth;
            private set => SetProperty(ref _CurrentMonth, value);
        }
        private string _CurrentMonth;

        public MonthWrapper SelectedMonth
        {
            get => _SelectedMonth;
            set => SetProperty(ref _SelectedMonth, value);
        }
        private MonthWrapper _SelectedMonth;

        public SalesReportByCustomerInAMonthViewModel()
        {
            Task.Run(async () =>
            {
                var input = await Services.APISalesReportRequest.GetSalesReport();
                var convertedOutput = Utilities.Util<mPOS.POCO.TrnSales>.ConvertToList(input);

                CustomerSales = GetWrappedOutput(convertedOutput);               
            });

            LoadMonths();

            _CurrentMonth = Months.ElementAt(DateTime.Now.Month).Value;
            _SelectedMonth = Months.ElementAt(DateTime.Now.Month - 1);
        }

        private List<Models.Wrappers.Report.SalesReportByCustomerInAMonthWrapper> GetWrappedOutput(List<mPOS.POCO.TrnSales> convertedOutput) 
        {
            var data = convertedOutput.SelectMany(x => x.TrnSalesLines,
                    (a, b) => new Models.Wrappers.Report.SalesReportByCustomerInAMonth()
                    {
                        CustomerId = a.CustomerId,
                        CustomerName = a.CustomerName,
                        ItemId = b.ItemId,
                        ItemName = b.MstItem.ItemDescription,
                        ItemQuantity = b.Quantity,
                        ItemSalesAmount = b.Amount
                    })
                .ToList();

            var groupedData = data.GroupBy(x => new { x.CustomerId, x.CustomerName, x.ItemId, x.ItemName })
                .Select(x => new Models.Wrappers.Report.SalesReportByCustomerInAMonth
                {
                    CustomerId = x.Key.CustomerId,
                    CustomerName = x.Key.CustomerName,
                    ItemId = x.Key.ItemId,
                    ItemName = x.Key.ItemName,
                    ItemQuantity = x.Sum(a => a.ItemQuantity),
                    ItemSalesAmount = x.Sum(a => a.ItemSalesAmount),
                })
                .ToList();

            var result = new List<Models.Wrappers.Report.SalesReportByCustomerInAMonthWrapper>();

            foreach (var item in groupedData.GroupBy(x => new { x.CustomerId, x.CustomerName}).Select(x => new {x.Key.CustomerId, x.Key.CustomerName})) 
            {
                result.Add(new Models.Wrappers.Report.SalesReportByCustomerInAMonthWrapper
                {
                    CustomerId = item.CustomerId,
                    CustomerName = item.CustomerName,
                    Details = groupedData.Where(x => x.CustomerId == item.CustomerId).OrderBy(x => x.ItemName).ToList()
                });
            }

            var rankedCustomer = result.OrderByDescending(x => x.TotalCustomerSales).ToList().ToArray();

            result.ForEach(x => x.Rank = "★ " + (Array.IndexOf(rankedCustomer, x) + 1).ToString());

            return result.OrderBy(x => x.CustomerName).ToList();
        }

        private void LoadMonths()
        {
            _Months = new List<MonthWrapper>();
            int ctr = 0;

            foreach (var month in DateTimeFormatInfo.CurrentInfo.MonthNames) 
            {
                _Months.Add(new MonthWrapper() { Key = ctr, Value = month });
                ctr++;
            }
        }
    }

    public class MonthWrapper
    {
        public int Key{ get; set; }
        public string Value { get; set; }
    }
}
