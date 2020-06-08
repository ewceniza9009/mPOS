using System.Collections.Generic;

namespace mPOS.POCO
{
    public class FilterMethods
    {
        public List<FilterOperation> Operations { get; set; }
    }

    public class FilterOperation
    {
        public FilterOperation()
        {

        }

        public FilterOperation(string filterName, Operation op)
        {
            FilterName = filterName;
            Operation = op;
        }
        public string FilterName { get; set; }
        public Operation Operation { get; set; } = Operation.Equals;
    }
}