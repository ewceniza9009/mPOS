using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mPOS.WebAPI.Repository
{
    public partial class MstItem
    {
        public List<POCO.MstUnit> GetUnits()
        {
            return Common.GetUnits();
        }

        public List<string> GetItemCategories()
        {
            List<string> result;

            using (var ctx = new Data.posDataContext())
            {
                result = ctx.MstItems
                    .GroupBy(x => x.Category).ToList()
                    .Select(y => y.Key)
                    .ToList();
            }

            return result.ToList();
        }

        public List<POCO.MstTax> GetTaxes()
        {
            return Common.GetTaxes();
        }
    }
}