namespace mPOSv2.Models.Page
{
    public static class Pager
    {
        public static int PageSize = 7;
        public static int CurrentPage = 1;
        public static double EndPage = 1;
        public static int Start => (CurrentPage - 1) * PageSize;
    }
}