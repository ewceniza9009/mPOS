using System;
using System.Collections.Generic;
using System.Linq;
using mPOS.POCO;
using mPOS.WebAPI.Data;
using mPOS.WebAPI.Mapping;
using mPOS.WebAPI.Utilities;

namespace mPOS.WebAPI.Repository
{
    public partial class MstCustomer : IRead<POCO.MstCustomer, MstCustomerFilter, FilterMethods>,
        IRepository<POCO.MstCustomer>
    {
        public POCO.MstCustomer Read(long id)
        {
            POCO.MstCustomer result;

            var mappingProfile = new MappingProfile<Data.MstCustomer, POCO.MstCustomer>();

            using (var ctx = new posDataContext())
            {
                var data = ctx.MstCustomers
                    .SingleOrDefault(x => x.Id == id);

                result = mappingProfile.mapper.Map<Data.MstCustomer, POCO.MstCustomer>(data);
            }

            return result;
        }

        public IEnumerable<POCO.MstCustomer> BulkRead(MstCustomerFilter filter = null,
            FilterMethods filterMethods = null)
        {
            IEnumerable<POCO.MstCustomer> result;

            var dynamicFilter = Filterer<MstCustomerFilter>.GetFilter(filter, filterMethods);
            var mappingProfile = new MappingProfile<Data.MstCustomer, POCO.MstCustomer>();

            using (var ctx = new posDataContext())
            {
                IEnumerable<Data.MstCustomer> data;

                if (filter != null)
                {
                    var enumerable = dynamicFilter as Filter[] ?? dynamicFilter.ToArray();
                    var filterExpression = ExpressionBuilder
                        .GetExpression<Data.MstCustomer>(enumerable.ToList()).Compile();

                    data = ctx.MstCustomers.Where(filterExpression);
                }
                else
                {
                    data = ctx.MstCustomers;
                }

                result = mappingProfile.mapper.Map<IEnumerable<Data.MstCustomer>, IEnumerable<POCO.MstCustomer>>(data);
            }

            return result;
        }

        public long Save(POCO.MstCustomer t)
        {
            Data.MstCustomer result;
            var mappingProfile = new MappingProfile<POCO.MstCustomer, Data.MstCustomer>();

            using (var ctx = new posDataContext())
            {
                if (t.Id != 0)
                {
                    result = ctx.MstCustomers.SingleOrDefault(x => x.Id == t.Id);

                    mappingProfile.mapper.Map(t, result);
                }
                else
                {
                    result = mappingProfile.mapper.Map<POCO.MstCustomer, Data.MstCustomer>(t);

                    result.TermId = 2;
                    result.TIN = "NA";
                    result.WithReward = false;
                    result.RewardConversion = 0;
                    result.AccountId = 64;
                    result.EntryUserId = 1;
                    result.EntryDateTime = DateTime.Now;
                    result.UpdateUserId = 1;
                    result.UpdateDateTime = DateTime.Now;

                    ctx.MstCustomers.InsertOnSubmit(result);
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
                    var result = ctx.MstCustomers.SingleOrDefault(x => x.Id == id);

                    ctx.MstCustomers.DeleteOnSubmit(result);
                    ctx.SubmitChanges();
                }
            }
        }
    }
}