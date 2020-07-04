using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mPOS.WebAPI.Repository
{
    public partial class TrnSales
    {
        public List<POCO.MstCustomer> GetCustomers()
        {
            return Common.GetCustomers();
        }

        public List<POCO.MstItem> GetItems()
        {
            return Common.GetItems();
        }

        public List<POCO.MstTax> GetTaxes()
        {
            return Common.GetTaxes();
        }
    }
}