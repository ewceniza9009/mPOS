﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mPOS.POCO
{
    public partial class TrnSalesLine
    {
        public string ItemDescription { get; set; }
        public string ItemDescriptionDisplay { get; set; }
        public string BarCode { get; set; }
        public string UnitName { get; set; }
        public bool IsDeleted { get; set; }
        public MstItem MstItem { get; set; }
    }
}
