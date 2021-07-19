using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mPOS.POCO
{
    public partial class TrnCollectionLine
    {
        public MstPayType MstPayType { get; set; }

        public bool IsCheckSelected { get; set; }

        public bool IsCCSelected { get; set; }

        public bool IsGCSelected { get; set; }

        public bool IsExchangeSelected { get; set; }

        public bool IsOtherSelected { get; set; }
        
    }
}
