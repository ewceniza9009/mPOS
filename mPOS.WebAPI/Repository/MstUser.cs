using System;
using System.Collections.Generic;
using System.Linq;
using mPOS.POCO;
using mPOS.WebAPI.Data;
using mPOS.WebAPI.Mapping;
using mPOS.WebAPI.Utilities;

namespace mPOS.WebAPI.Repository
{
    public class MstUser : IRead<POCO.MstUser, MstUserFilter, FilterMethods>, IRepository<POCO.MstUser>
    {
        public POCO.MstUser Read(long id)
        {
            POCO.MstUser result;

            var mappingProfile = new MappingProfile<Data.MstUser, POCO.MstUser>();

            using (var ctx = new posDataContext())
            {
                var data = ctx.MstUsers
                    .SingleOrDefault(x => x.Id == id);

                result = mappingProfile.mapper.Map<Data.MstUser, POCO.MstUser>(data);
            }

            return result;
        }

        public IEnumerable<POCO.MstUser> BulkRead(MstUserFilter filter = null, FilterMethods filterMethods = null)
        {
            IEnumerable<POCO.MstUser> result;

            var dynamicFilter = Filterer<MstUserFilter>.GetFilter(filter, filterMethods);
            var mappingProfile = new MappingProfile<Data.MstUser, POCO.MstUser>();

            using (var ctx = new posDataContext())
            {
                IEnumerable<Data.MstUser> data;

                if (filter != null)
                {
                    var enumerable = dynamicFilter as Filter[] ?? dynamicFilter.ToArray();
                    var filterExpression = ExpressionBuilder
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
            POCO.MstUser result;

            var mappingProfile = new MappingProfile<Data.MstUser, POCO.MstUser>();

            using (var ctx = new posDataContext())
            {
                var data = ctx.MstUsers.SingleOrDefault(x =>
                    x.UserName == user &&
                    x.Password == password);
                ;

                result = mappingProfile.mapper.Map<Data.MstUser, POCO.MstUser>(data);
            }

            return result;
        }
    }
}