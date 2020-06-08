using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mPOS.POCO;
using mPOS.WebAPI.Utilities;

namespace mPOS.WebAPI.Repository
{
    public class MstAccount : IRead<POCO.MstAccount, POCO.MstAccountFilter, POCO.FilterMethods>, IRepository<POCO.MstAccount>
    {
        public POCO.MstAccount Read(long id)
        {
            POCO.MstAccount result;

            var mappingProfile = new Mapping.MappingProfile<Data.MstAccount, POCO.MstAccount>();

            using (var ctx = new Data.posDataContext())
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

            var dynamicFilter = Utilities.Filterer<POCO.MstAccountFilter>.GetFilter(filter, filterMethods);
            var mappingProfile = new Mapping.MappingProfile<Data.MstAccount, POCO.MstAccount>();

            using (var ctx = new Data.posDataContext())
            {
                IEnumerable<Data.MstAccount> data;

                if (filter != null)
                {
                    var enumerable = dynamicFilter as Filter[] ?? dynamicFilter.ToArray();
                    var filterExpression = Utilities.ExpressionBuilder
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