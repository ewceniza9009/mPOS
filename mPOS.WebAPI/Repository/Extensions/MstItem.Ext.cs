using System.Collections.Generic;
using System.Linq;
using mPOS.WebAPI.Data;
using MstTax = mPOS.POCO.MstTax;
using MstUnit = mPOS.POCO.MstUnit;

namespace mPOS.WebAPI.Repository
{
    public partial class MstItem
    {
        public List<MstUnit> GetUnits()
        {
            return Common.GetUnits();
        }

        public List<string> GetItemCategories()
        {
            List<string> result;

            using (var ctx = new posDataContext())
            {
                result = ctx.MstItems
                    .GroupBy(x => x.Category).ToList()
                    .Select(y => y.Key)
                    .ToList();
            }

            return result.ToList();
        }

        public List<MstTax> GetTaxes()
        {
            return Common.GetTaxes();
        }
    }
}