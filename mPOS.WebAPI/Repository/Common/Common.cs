using System.Collections.Generic;
using System.Linq;
using mPOS.WebAPI.Data;
using mPOS.WebAPI.Mapping;
using MstTax = mPOS.POCO.MstTax;
using MstUnit = mPOS.POCO.MstUnit;
using MstTerm = mPOS.POCO.MstTerm;
using MstPayType = mPOS.POCO.MstPayType;

namespace mPOS.WebAPI.Repository
{
    public static class Common
    {
        public static List<MstUnit> GetUnits()
        {
            IEnumerable<MstUnit> result;

            var mappingProfile = new MappingProfile<Data.MstUnit, MstUnit>();

            using (var ctx = new posDataContext())
            {
                var data = ctx.MstUnits;

                result = mappingProfile.mapper.Map<IEnumerable<Data.MstUnit>, IEnumerable<MstUnit>>(data);
            }

            return result.ToList();
        }

        public static List<POCO.MstCustomer> GetCustomers()
        {
            IEnumerable<POCO.MstCustomer> result;

            var mappingProfile = new MappingProfile<Data.MstCustomer, POCO.MstCustomer>();

            using (var ctx = new posDataContext())
            {
                var data = ctx.MstCustomers;

                result = mappingProfile.mapper.Map<IEnumerable<Data.MstCustomer>, IEnumerable<POCO.MstCustomer>>(data);
            }

            return result.ToList();
        }

        public static List<POCO.MstItem> GetItems()
        {
            IEnumerable<POCO.MstItem> result;

            var mappingProfile = new MappingProfile<Data.MstItem, POCO.MstItem>();

            using (var ctx = new posDataContext())
            {
                var data = ctx.MstItems;

                result = mappingProfile.mapper.Map<IEnumerable<Data.MstItem>, IEnumerable<POCO.MstItem>>(data);
            }

            return result.ToList();
        }

        public static List<MstTax> GetTaxes()
        {
            IEnumerable<MstTax> result;

            var mappingProfile = new MappingProfile<Data.MstTax, MstTax>();

            using (var ctx = new posDataContext())
            {
                var data = ctx.MstTaxes;

                result = mappingProfile.mapper.Map<IEnumerable<Data.MstTax>, IEnumerable<MstTax>>(data);
            }

            return result.ToList();
        }

        public static List<MstTerm> GetTerms()
        {
            IEnumerable<MstTerm> result;

            var mappingProfile = new MappingProfile<Data.MstTerm, MstTerm>();

            using (var ctx = new posDataContext())
            {
                var data = ctx.MstTerms;

                result = mappingProfile.mapper.Map<IEnumerable<Data.MstTerm>, IEnumerable<MstTerm>>(data);
            }

            return result.ToList();
        }

        public static List<MstPayType> GetPayTypes()
        {
            IEnumerable<MstPayType> result;

            var mappingProfile = new MappingProfile<Data.MstPayType, MstPayType>();

            using (var ctx = new posDataContext())
            {
                var data = ctx.MstPayTypes;

                result = mappingProfile.mapper.Map<IEnumerable<Data.MstPayType>, IEnumerable<MstPayType>>(data);
            }

            return result.ToList();
        }
    }
}