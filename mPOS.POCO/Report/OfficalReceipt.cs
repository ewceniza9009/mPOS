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
        public string TotalSales { get; set; }
        public string TotalDiscount { get; set; }
        public List<TenderLine> TenderLines { get; set; }
        public string ChangeAmount { get; set; }
        public List<VatLine> VatLines { get; set; }
        public SeniorCitizen SeniorCitizenDetail { get; set; }
        public string Terminal { get; set; }
        public string Customer { get; set; }

    }

    public class LineItem 
    {
        public string ItemDescription { get; set; }
        public string Quantity { get; set; }
        public string PriceDescription { get; set; }
        public string Amount { get; set; }
    }

    public class TenderLine 
    {
        public string PayType { get; set; }
        public string Amount { get; set; }

    }

    public class VatLine
    {
        public string Tax { get; set; }
        public string AmountLessTax { get; set; }
        public string TotalTaxAmount { get; set; }
    }

    public class SeniorCitizen 
    {
        public string SeniorCitizenId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}