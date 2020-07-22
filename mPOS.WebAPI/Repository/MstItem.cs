using System;
using System.Collections.Generic;
using System.Linq;
using mPOS.POCO;
using mPOS.WebAPI.Data;
using mPOS.WebAPI.Mapping;
using mPOS.WebAPI.Utilities;

namespace mPOS.WebAPI.Repository
{
    public partial class MstItem : IRead<POCO.MstItem, MstItemFilter, FilterMethods>, IRepository<POCO.MstItem>
    {
        public POCO.MstItem Read(long id)
        {
            POCO.MstItem result;

            var mappingProfile = new MappingProfile<Data.MstItem, POCO.MstItem>();

            using (var ctx = new posDataContext())
            {
                var data = ctx.MstItems
                    .SingleOrDefault(x => x.Id == id);

                result = mappingProfile.mapper.Map<Data.MstItem, POCO.MstItem>(data);
            }

            return result;
        }

        public IEnumerable<POCO.MstItem> BulkRead(MstItemFilter filter = null, FilterMethods filterMethods = null)
        {
            IEnumerable<POCO.MstItem> result;

            var dynamicFilter = Filterer<MstItemFilter>.GetFilter(filter, filterMethods);
            var mappingProfile = new MappingProfile<Data.MstItem, POCO.MstItem>();

            using (var ctx = new posDataContext())
            {
                IEnumerable<Data.MstItem> data;

                if (filter != null)
                {
                    var enumerable = dynamicFilter as Filter[] ?? dynamicFilter.ToArray();
                    var filterExpression = ExpressionBuilder
                        .GetExpression<Data.MstItem>(enumerable.ToList()).Compile();

                    data = ctx.MstItems.Where(filterExpression);
                }
                else
                {
                    data = ctx.MstItems;
                }

                result = mappingProfile.mapper.Map<IEnumerable<Data.MstItem>, IEnumerable<POCO.MstItem>>(data);
            }

            return result;
        }

        public long Save(POCO.MstItem t)
        {
            Data.MstItem result;
            var mappingProfile = new MappingProfile<POCO.MstItem, Data.MstItem>();

            using (var ctx = new posDataContext())
            {
                if (t.Id != 0)
                {
                    result = ctx.MstItems.SingleOrDefault(x => x.Id == t.Id);

                    mappingProfile.mapper.Map(t, result);
                }
                else
                {
                    result = mappingProfile.mapper.Map<POCO.MstItem, Data.MstItem>(t);

                    result.SalesAccountId = 159;
                    result.AssetAccountId = 74;
                    result.CostAccountId = 238;
                    result.InTaxId = 9;
                    result.OutTaxId = 9;
                    result.DefaultSupplierId = 23;
                    result.IsPackage = false;
                    result.ImagePath = "NA";
                    result.EntryUserId = 1;
                    result.EntryDateTime = DateTime.Now;
                    result.UpdateUserId = 1;
                    result.UpdateDateTime = DateTime.Now;

                    ctx.MstItems.InsertOnSubmit(result);
                }

                ctx.SubmitChanges();
            }

            return result?.Id ?? 0;
        }

        public void Delete(long id)
        {
            using (var ctx = new posDataContext())
            {
                if (id > 0)
                {
                    var result = ctx.MstItems.SingleOrDefault(x => x.Id == id);

                    ctx.MstItems.DeleteOnSubmit(result);
                    ctx.SubmitChanges();
                }
            }
        }
    }
}