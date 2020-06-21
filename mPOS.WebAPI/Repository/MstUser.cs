using System;
using System.Collections.Generic;
using System.Linq;
using mPOS.WebAPI.Utilities;

namespace mPOS.WebAPI.Repository
{
    public class MstUser : IRead<POCO.MstUser, POCO.MstUserFilter, POCO.FilterMethods>, IRepository<POCO.MstUser>
    {
        public POCO.MstUser Read(long id)
        {
            POCO.MstUser result;

            var mappingProfile = new Mapping.MappingProfile<Data.MstUser, POCO.MstUser>();

            using (var ctx = new Data.posDataContext())
            {
                var data = ctx.MstUsers
                    .SingleOrDefault(x => x.Id == id);

                result = mappingProfile.mapper.Map<Data.MstUser, POCO.MstUser>(data);
            }

            return result;
        }

        public IEnumerable<POCO.MstUser> BulkRead(POCO.MstUserFilter filter = null, POCO.FilterMethods filterMethods = null)
        {
            IEnumerable<POCO.MstUser> result;

            var dynamicFilter = Utilities.Filterer<POCO.MstUserFilter>.GetFilter(filter, filterMethods);
            var mappingProfile = new Mapping.MappingProfile<Data.MstUser, POCO.MstUser>();

            using (var ctx = new Data.posDataContext())
            {
                IEnumerable<Data.MstUser> data;

                if (filter != null)
                {
                    var enumerable = dynamicFilter as Filter[] ?? dynamicFilter.ToArray();
                    var filterExpression = Utilities.ExpressionBuilder
                        .GetExpression<Data.MstUser>(enumerable.ToList()).Compile();

                    data = ctx.MstUsers.Where(filterExpression);
                }
                else
                {
                    data = ctx.MstUsers;
                }

                result = mappingProfile.mapper.Map<IEnumerable<Data.MstUser>, IEnumerable<POCO.MstUser>>(data);
            }

            return result;
        }

        public long Save(POCO.MstUser t)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public POCO.MstUser IsLoginSuccess(string user, string password)
        {
            var result = new POCO.MstUser();

            var mappingProfile = new Mapping.MappingProfile<Data.MstUser, POCO.MstUser>();

            using (var ctx = new Data.posDataContext())
            {
                var data = ctx.MstUsers.SingleOrDefault(x =>
                    x.UserName == user &&
                    x.Password == password); ;

                result = mappingProfile.mapper.Map<Data.MstUser, POCO.MstUser>(data);
            }

            return result;
        }
    }
}