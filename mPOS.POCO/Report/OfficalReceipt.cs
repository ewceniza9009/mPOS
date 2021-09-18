using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mPOS.POCO.Report
{
    public class OfficalReceipt
    {
        public string ORNumber { get; set; }
        public string UpdateDateTime { get; set; }
        public string Remarks { get; set; }
        public List<LineItem> LineItems { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalDiscount { get; set; }
        public List<TenderLine> TenderLines { get; set; }
        public List<VatLine> VatLines { get; set; }
        public SeniorCitizen SeniorCitizenDetail { get; set; }

    }

    public class LineItem 
    {
        public string ItemDescription { get; set; }
        public decimal Quantity { get; set; }
        public string PriceDescription { get; set; }
        public decimal Amount { get; set; }
    }

    public class TenderLine 
    {
        public string PayType { get; set; }
        public decimal Amount { get; set; }

    }

    public class VatLine
    {
        public string Tax { get; set; }
        public decimal AmountLessTax { get; set; }
        public decimal TotalTaxAmount { get; set; }
    }

    public class SeniorCitizen 
    {
        public string SeniorCitizenId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}