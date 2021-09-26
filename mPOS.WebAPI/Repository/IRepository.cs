using System.Collections.Generic;

namespace mPOS.WebAPI.Repository
{
    /// <summary>
    /// Read interface
    /// </summary>
    /// <typeparam name="T">Type for output</typeparam>
    /// <typeparam name="TFilter">Model type for filter</typeparam>
    /// <typeparam name="TFilterMethods">Type for filter methods</typeparam>
    public interface IRead<out T, in TFilter, in TFilterMethods>
    {
        /// <summary>
        /// Read single record
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Type of T</returns>
        T Read(long id);

        /// <summary>
        /// Read multiple records
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="filterMethods">Filter methods</param>
        /// <returns></returns>
        IEnumerable<T> BulkRead(TFilter filter, TFilterMethods filterMethods);
    }

    /// <summary>
    /// Command interface
    /// </summary>
    /// <typeparam name="T">Type for input</typeparam>
    public interface IRepository<in T>
    {
        /// <summary>
        /// Save entity
        /// </summary>
        /// <param name="t">Entity</param>
        /// <returns></returns>
        long Save(T t);
        
        /// <summary>
        /// Delete entity by Id
        /// </summary>
        /// <param name="id">Entity Id</param>
        void Delete(long id);
    }
}