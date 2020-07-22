using System;
using System.Collections.Generic;
using System.Linq;
using mPOS.POCO;
using mPOS.WebAPI.Data;
using mPOS.WebAPI.Mapping;
using mPOS.WebAPI.Utilities;

namespace mPOS.WebAPI.Repository
{
    public class MstAccount : IRead<POCO.MstAccount, MstAccountFilter, FilterMethods>, IRepository<POCO.MstAccount>
    {
        public POCO.MstAccount Read(long id)
        {
            POCO.MstAccount result;

            var mappingProfile = new MappingProfile<Data.MstAccount, POCO.MstAccount>();

            using (var ctx = new posDataContext())
            {
                var data = ctx.MstAccounts
                    .SingleOrDefault(x => x.Id == id);

                result = mappingProfile.mapper.Map<Data.MstAccount, POCO.MstAccount>(data);
            }

            return result;
        }

        public IEnumerable<POCO.MstAccount> BulkRead(MstAccountFilter filter = null, FilterMethods filterMethods = null)
        {
            IEnumerable<POCO.MstAccount> result;

            var dynamicFilter = Filterer<MstAccountFilter>.GetFilter(filter, filterMethods);
            var mappingProfile = new MappingProfile<Data.MstAccount, POCO.MstAccount>();

            using (var ctx = new posDataContext())
            {
                IEnumerable<Data.MstAccount> data;

                if (filter != null)
                {
                    var enumerable = dynamicFilter as Filter[] ?? dynamicFilter.ToArray();
                    var filterExpression = ExpressionBuilder
                        .GetExpression<Data.MstAccount>(enumerable.ToList()).Compile();

                    data = ctx.MstAccounts.Where(filterExpression);
                }
                else
                {
                    data = ctx.MstAccounts;
                }

                result = mappingProfile.mapper.Map<IEnumerable<Data.MstAccount>, IEnumerable<POCO.MstAccount>>(data);
            }

            return result;
        }

        public long Save(POCO.MstAccount t)
        {
            throw new NotImplementedException();
        }


        public void Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}