using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mPOS.POCO;

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

        public List<POCO.MstDiscount> GetDiscounts()
        {
            IEnumerable<POCO.MstDiscount> result;

            var mappingProfile = new Mapping.MappingProfile<Data.MstDiscount, POCO.MstDiscount>();

            using (var ctx = new Data.posDataContext())
            {
                var data = ctx.MstDiscounts;

                result = mappingProfile.mapper.Map<IEnumerable<Data.MstDiscount>, IEnumerable<POCO.MstDiscount>>(data);
            }

            return result.ToList();
        }
    }
}