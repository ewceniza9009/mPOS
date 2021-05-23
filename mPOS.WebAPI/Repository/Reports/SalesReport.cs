using mPOS.POCO;
using mPOS.WebAPI.Data;
using mPOS.WebAPI.Mapping;
using mPOS.WebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace mPOS.WebAPI.Repository.Reports
{
    public class SalesReport
    {
        private TrnSales _TrnSales;

        public SalesReport(TrnSales trnSales)
        {
            _TrnSales = trnSales;
        }

        public IEnumerable<POCO.TrnSales> GetSalesReport(TrnSalesFilter filter = null, FilterMethods filterMethods = null) 
        {
            return _TrnSales.BulkRead(filter, filterMethods);
        }

        public IEnumerable<POCO.TrnSales> MonthlyReport() 
        {
            return null;
        }

        public IEnumerable<POCO.TrnSales> GetSalesReport(DateTime date)
        {
            IEnumerable<POCO.TrnSales> result;

            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var mappingProfile = new MappingProfileForTrnSales();

            using (var ctx = new posDataContext())
            {
                ctx.DeferredLoadingEnabled = false;

                var trnSalesIncludes = new DataLoadOptions();
                trnSalesIncludes.LoadWith<Data.TrnSale>(x => x.TrnSalesLines);
                trnSalesIncludes.LoadWith<Data.TrnSale>(x => x.MstCustomer);
                trnSalesIncludes.LoadWith<Data.TrnSalesLine>(x => x.MstItem);

                ctx.LoadOptions = trnSalesIncludes;
                
                result = mappingProfile.mapper.Map<List<POCO.TrnSales>>(ctx.TrnSales
                    .Where(x => x.SalesDate >= firstDayOfMonth && x.SalesDate <= lastDayOfMonth));
            }

            return result;
        }
    }
}