using System;

namespace mPOSv2.MenuItems
{
    public class MenuPageItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public Type TargetType { get; set; }
    }
}