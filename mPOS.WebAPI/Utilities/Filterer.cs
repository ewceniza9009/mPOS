using System.Collections.Generic;
using System.Linq;
using mPOS.POCO;

namespace mPOS.WebAPI.Utilities
{
    public static class Filterer<T>
    {
        public static IEnumerable<Filter> GetFilter(T filter, FilterMethods filterMethods)
        {
            var filterType = typeof(T);
            var filterProperties = filterType.GetProperties();

            foreach (var prop in filterProperties)
            {
                var propValue = prop.GetValue(filter);
                var operations = filterMethods.Operations;
                var operation = operations
                    .SingleOrDefault(x => x.FilterName == prop.Name);

                if (propValue == null) continue;
                if (prop.Name == "filterMethods") break;
                if (operation != null)
                    yield return new Filter
                    {
                        PropertyName = prop.Name,
                        Operation = operation.Operation,
                        Value = propValue
                    };
            }
        }
    }
}