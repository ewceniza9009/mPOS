using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mPOS.POCO;
using mPOS.WebAPI.Utilities;
using WebGrease.Css.Extensions;

namespace mPOS.WebAPI.Repository
{
    public class TrnSales : IRead<POCO.TrnSales, POCO.TrnSalesFilter, POCO.FilterMethods>, IRepository<POCO.TrnSales>
    {
        public POCO.TrnSales Read(long id)
        {
            POCO.TrnSales result;

            var mappingProfile = new Mapping.MappingProfileForTrnSales();;

            using (var ctx = new Data.posDataContext())
            {
                var data = ctx.TrnSales
                    .SingleOrDefault(x => x.Id == id);

                result = mappingProfile.mapper.Map<POCO.TrnSales>(data);
            }

            return result;
        }

        public IEnumerable<POCO.TrnSales> BulkRead(TrnSalesFilter filter = null, FilterMethods filterMethods = null)
        {
            IEnumerable<POCO.TrnSales> result;

            var dynamicFilter = Utilities.Filterer<POCO.TrnSalesFilter>.GetFilter(filter, filterMethods);
            var mappingProfile = new Mapping.MappingProfileForTrnSales(); 

            using (var ctx = new Data.posDataContext())
            {
                IEnumerable<Data.TrnSale> data;

                if (filter != null)
                {
                    var enumerable = dynamicFilter as Filter[] ?? dynamicFilter.ToArray();
                    var filterExpression = Utilities.ExpressionBuilder
                        .GetExpression<Data.TrnSale>(enumerable.ToList()).Compile();

                    data = ctx.TrnSales.Where(filterExpression);
                }
                else
                {
                    data = ctx.TrnSales;
                }

                result = mappingProfile.mapper.Map<List<POCO.TrnSales>>(data);
            }

            return result;
        }

        //TODO: Complete TrnSales Saving 
        public long Save(POCO.TrnSales t)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            using (var ctx = new Data.posDataContext())
            {
                if (id > 0)
                {
                    var result = ctx.TrnSales.SingleOrDefault(x => x.Id == id);

                    ctx.TrnSales.DeleteOnSubmit(result);
                    ctx.SubmitChanges();
                }
            }
        }
    }
}