using System.Collections.Generic;
using System.Linq;
using mPOS.WebAPI.Data;
using mPOS.WebAPI.Mapping;
using MstDiscount = mPOS.POCO.MstDiscount;
using MstTax = mPOS.POCO.MstTax;

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

        public List<MstTax> GetTaxes()
        {
            return Common.GetTaxes();
        }

        public List<MstDiscount> GetDiscounts()
        {
            IEnumerable<MstDiscount> result;

            var mappingProfile = new MappingProfile<Data.MstDiscount, MstDiscount>();

            using (var ctx = new posDataContext())
            {
                var data = ctx.MstDiscounts;

                result = mappingProfile.mapper.Map<IEnumerable<Data.MstDiscount>, IEnumerable<MstDiscount>>(data);
            }

            return result.ToList();
        }
    }
}