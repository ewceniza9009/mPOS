using mPOS.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mPOS.WebAPI.Repository
{
    public partial class TrnCollection : IRead<POCO.TrnCollection, TrnSalesFilter, FilterMethods>, IRepository<POCO.TrnCollection>
    {
        public POCO.TrnCollection Read(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<POCO.TrnCollection> BulkRead(TrnSalesFilter filter, FilterMethods filterMethods)
        {
            throw new NotImplementedException();
        }

        public long Save(POCO.TrnCollection t)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}