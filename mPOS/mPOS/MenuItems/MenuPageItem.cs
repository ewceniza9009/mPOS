using System;
using System.Collections.Generic;
using System.Text;

namespace mPOS.MenuItems
{
    public class MenuPageItem
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public Type TargetType { get; set; }
    }
}
