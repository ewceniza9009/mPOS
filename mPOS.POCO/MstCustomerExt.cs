using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mPOS.POCO
{
    public partial class MstCustomer
    {
        public string Initials
        {
            get => Customer?.Substring(0, 2).ToUpper();
        }
    }
}
