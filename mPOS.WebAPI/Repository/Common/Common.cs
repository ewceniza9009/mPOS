﻿using System.Collections.Generic;
using System.Linq;
using mPOS.WebAPI.Data;
using mPOS.WebAPI.Mapping;
using MstTax = mPOS.POCO.MstTax;
using MstUnit = mPOS.POCO.MstUnit;

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
    }
}