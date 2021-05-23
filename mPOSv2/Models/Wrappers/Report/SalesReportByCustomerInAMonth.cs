using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mPOSv2.Models.Wrappers.Report
{
    public class SalesReportByCustomerInAMonth
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal ItemQuantity { get; set; }
        public decimal ItemSalesAmount { get; set; }
    }

    public class SalesReportByCustomerInAMonthWrapper
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<SalesReportByCustomerInAMonth> Details { get; set; }
        public decimal TotalCustomerSales => Details.Sum(x => x.ItemSalesAmount);
        public string Rank { get; set; }
    }
}
