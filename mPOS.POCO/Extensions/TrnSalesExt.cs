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

        public List<TrnSalesLine> TrnSalesLines { get; set; }

        public decimal SalesAmount
        {
            get => TrnSalesLines.Sum(x => x.Amount);
        }

        public bool IsNotTendered { get; set; } = true;

        public string InvColor
        {
            get 
            {
                var color = "Black";

                if (!IsNotTendered) 
                {
                    color = "Green";
                }

                return color;
            }
        }

        public string SalesNumberDisplay 
        {
            get 
            {
                var display = SalesNumber;

                if (!IsNotTendered)
                {
                    display = SalesNumber += "-T";
                }

                return display;
            }
        }
    }
}
