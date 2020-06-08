using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mPOS.WebAPI.Repository
{
    public interface IRead<out T, in TFilter, in TFilterMethods>
    {
        T Read(long id);

        IEnumerable<T> BulkRead(TFilter filter, TFilterMethods filterMethods);
    }

    public interface IRepository<in T>
    {
        long Save(T t);
        void Delete(long id);
    }
}
