using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mPOS.POCO
{
    public partial class TrnSales
    {
        public string CustomerName { get; set; }

        public decimal SalesAmount
        {
            get => TrnSalesLines.Sum(x => x.Amount);
        }
    }
}
