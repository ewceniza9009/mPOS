using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public SalesReportByCustomerInAMonthViewModel()
        {
            Task.Run(async () =>
            {
                var input = await Services.APISalesReportRequest.GetSalesReport();
                var convertedOutput = Utilities.Util<mPOS.POCO.TrnSales>.ConvertToList(input);

                CustomerSales = GetWrappedOutput(convertedOutput);
            });            
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
    }
}
