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
            return Common.GetItemCategories();
        }

        public List<POCO.MstTax> GetTaxes()
        {
            return Common.GetTaxes();
        }
    }
}