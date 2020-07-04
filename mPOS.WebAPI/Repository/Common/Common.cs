using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mPOS.POCO;

namespace mPOS.WebAPI.Repository
{
    public static class Common
    {
        public static List<POCO.MstUnit> GetUnits()
        {
            IEnumerable<POCO.MstUnit> result;

            var mappingProfile = new Mapping.MappingProfile<Data.MstUnit, POCO.MstUnit>();

            using (var ctx = new Data.posDataContext())
            {
                var data = ctx.MstUnits;

                result = mappingProfile.mapper.Map<IEnumerable<Data.MstUnit>, IEnumerable<POCO.MstUnit>>(data);
            }

            return result.ToList();
        }

        public static List<string> GetItemCategories()
        {
            List<string> result;

            using (var ctx = new Data.posDataContext())
            {

                result = ctx.MstItems
                    .GroupBy(x => x.Category).ToList()
                    .Select(y => y.Key)
                    .ToList();
            }

            return result.ToList();
        }

        public static List<POCO.MstCustomer> GetCustomers()
        {
            IEnumerable<POCO.MstCustomer> result;

            var mappingProfile = new Mapping.MappingProfile<Data.MstCustomer, POCO.MstCustomer>();

            using (var ctx = new Data.posDataContext())
            {
                var data = ctx.MstCustomers;

                result = mappingProfile.mapper.Map<IEnumerable<Data.MstCustomer>, IEnumerable<POCO.MstCustomer>>(data);
            }

            return result.ToList();
        }

        public static List<POCO.MstItem> GetItems()
        {
            IEnumerable<POCO.MstItem> result;

            var mappingProfile = new Mapping.MappingProfile<Data.MstItem, POCO.MstItem>();

            using (var ctx = new Data.posDataContext())
            {
                var data = ctx.MstItems;

                result = mappingProfile.mapper.Map<IEnumerable<Data.MstItem>, IEnumerable<POCO.MstItem>>(data);
            }

            return result.ToList();
        }

        public static List<MstTax> GetTaxes()
        {
            IEnumerable<POCO.MstTax> result;

            var mappingProfile = new Mapping.MappingProfile<Data.MstTax, POCO.MstTax>();

            using (var ctx = new Data.posDataContext())
            {
                var data = ctx.MstTaxes;

                result = mappingProfile.mapper.Map<IEnumerable<Data.MstTax>, IEnumerable<POCO.MstTax>>(data);
            }

            return result.ToList();
        }
    }
}